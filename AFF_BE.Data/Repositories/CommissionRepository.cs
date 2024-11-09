using AFF_BE.Core.Data.Commission;
using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.Data.Payment;
using AFF_BE.Core.Data.Tree;
using AFF_BE.Core.IRepositories;
using AFF_BE.Core.ISeedWorks;
using AFF_BE.Core.Models.Commission;
using AFF_BE.Data.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AFF_BE.Core.Models.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AFF_BE.Data.Repositories
{
    public class CommissionRepository : RepositoryBase<IndirectCommission, int>, ICommissionRepository
    {
        private readonly IMapper _mapper;

        private IRepositoryBase<IndirectCommission, int> _repositoryBaseImplementation;

        public CommissionRepository(AffContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) :
            base(context, httpContextAccessor)
        {
            _mapper = mapper;
        }

        public async Task CalculateCommissionAsync(int orderId)
        {
            // Lấy thông tin đơn hàng và người mua
            var order = await _dbContext.Orders
                .Where(o => o.Id == orderId)
                .Include(o => o.User)
                .FirstOrDefaultAsync();

            if (order == null) return;

            var user = order.User;

            var referrer = await _dbContext.Users
               .Where(o => o.PhoneNumber == user.ReferralCode)
               .FirstOrDefaultAsync();

            var commissionPercentage = 0.25;

            var directCommissionAmount = (double)order.TotalAmount * commissionPercentage;

            var directCommission = await _dbContext.DirectCommissions.Where(o => o.Id == referrer.Id)
                .Include(o => o.User)
                .FirstOrDefaultAsync();

            if (directCommission == null)
            {
                directCommission = new DirectCommission
                {
                    UserId = referrer.Id,
                    Amount = directCommissionAmount
                };

                await _dbContext.DirectCommissions.AddAsync(directCommission);
            }
            else
            {
                directCommission.Amount += directCommissionAmount;

                _dbContext.DirectCommissions.Update(directCommission);
            }

            await _dbContext.SaveChangesAsync();

            // Lấy tổng số sản phẩm người mua đã mua trước đó (trừ đơn hàng hiện tại)
            var totalPurchasedBefore = await _dbContext.Orders
                .Where(o => o.UserId == order.UserId && o.Id != orderId)
                .SumAsync(o => o.TotalQuantity);

            int purchaseCount = totalPurchasedBefore;

            // Lấy BranchPath của người mua (từ Node gốc đến Node hiện tại)
            var buyerTreeNode = await _dbContext.TreeNodes
                .Where(t => t.UserId == order.UserId)
                .FirstOrDefaultAsync();

            if (buyerTreeNode == null || string.IsNullOrEmpty(buyerTreeNode.BranchPath)) return;

            // Tách BranchPath ra và loại bỏ ID của người mua
            var parentIds = buyerTreeNode.BranchPath
                .Split('/')
                .Where(id => !string.IsNullOrEmpty(id))
                .Select(int.Parse)
                .Where(id => id != user.Id) // Loại bỏ ID của chính người mua
                .ToList();

            double indirectCommissionAmountPerProduct = (double)order.TotalAmount / order.TotalQuantity * commissionPercentage;
            int maxCommissionLevels = order.TotalQuantity;
            int levelsDistributed = 0;

            // Cờ để xác định nếu tầng root là người nhận cuối cùng
            bool isRoot = false;

            foreach (var parentId in parentIds.Reverse<int>())
            {
                if (levelsDistributed >= maxCommissionLevels) break;

                var parentNode = await _dbContext.TreeNodes
                    .Where(t => t.Id == parentId)
                    .FirstOrDefaultAsync();

                if (parentNode == null || parentNode.UserId == user.Id) continue;

                if (purchaseCount > 0)
                {
                    purchaseCount--;
                    continue;
                }

                // Kiểm tra nếu node hiện tại là root
                isRoot = parentNode.Position == "root";

                // Nếu là root, gộp hoa hồng còn lại vào một bản ghi
                if (isRoot)
                {
                    var rootCommissionAmount = indirectCommissionAmountPerProduct * (maxCommissionLevels - levelsDistributed);
                    var rootIndirectCommissionDto = new CreateIndirectCommissionDto
                    {
                        OrderId = order.Id,
                        Price = (double)order.TotalAmount,
                        Commission = rootCommissionAmount,
                        UserBuyId = order.UserId,
                        UserReciveId = parentNode.UserId ?? 0,
                        BuyNumber = order.TotalQuantity,
                        Quantity = order.TotalQuantity,
                        Code = "HHGT" + new Random().Next(10, 99) + "ID" + order.Id
                    };

                    if (rootIndirectCommissionDto.UserReciveId != order.UserId)
                    {
                        var rootIndirectCommission = _mapper.Map<IndirectCommission>(rootIndirectCommissionDto);
                        await CreateAsync(rootIndirectCommission);
                    }
                    break;
                }
                else
                {
                    // Nếu không phải root, tạo hoa hồng cho node cha hiện tại
                    var indirectCommissionDto = new CreateIndirectCommissionDto
                    {
                        OrderId = order.Id,
                        Price = (double)order.TotalAmount,
                        Commission = indirectCommissionAmountPerProduct,
                        UserBuyId = order.UserId,
                        UserReciveId = parentNode.UserId ?? 0,
                        BuyNumber = order.TotalQuantity,
                        Quantity = order.TotalQuantity,
                        Code = "HHGT" + new Random().Next(10, 99) + "ID" + order.Id
                    };

                    if (indirectCommissionDto.UserReciveId != order.UserId)
                    {
                        var indirectCommission = _mapper.Map<IndirectCommission>(indirectCommissionDto);
                        await CreateAsync(indirectCommission);
                        levelsDistributed++;
                    }
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<GetCommissionByUserDto> GetCommissionByUserId(int userId)
        {

            var userEntity = await _dbContext.Users
                .Include(u => u.IndirectCommissions)
                .Include(u => u.DirectCommission)
                .Where(u => u.Id == userId)

                .SingleOrDefaultAsync();


            var user = _mapper.Map<GetCommissionByUserDto>(userEntity);
            return user;
        }
    }
}