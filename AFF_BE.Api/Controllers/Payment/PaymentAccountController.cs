using AFF_BE.Core.ISeedWorks;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Content.Banner;
using AFF_BE.Core.Models.Payment;
using AFF_BE.Data;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AFF_BE.Api.Controllers.Payment
{
    [Route("api/payment-account")]
    [ApiController]
    public class PaymentAccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentAccountController(AffContext context, IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("paging")]
        public async Task<ApiResult<PagingResult<PaymentAccountDto>>> Get([FromQuery] GetPaymentAccountRequest request)
        {
            var result = await _unitOfWork.PaymentAccounts.GetAllPaging(request.BankCode,request.BankName,request.SortBy, request.OrderBy,request.PageIndex, request.PageSize);

            return new ApiResult<PagingResult<PaymentAccountDto>>()
            {
                Status = true,
                Message = "Danh sách BankAccount đã được lấy thành công!",
                Data = result
            };
        }
        [HttpPost("create")]
        public async Task<ApiResult<bool>> Create([FromBody] CreatePaymentAccountRequest request)
        {

            var paymentAccount = _mapper.Map<CreatePaymentAccountRequest, PaymentAccount>(request);
            
            await _unitOfWork.PaymentAccounts.CreateAsync(paymentAccount);

            await _unitOfWork.CompleteAsync();


            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Tạo mới PaymentAccount thành công!",
                Data = true
            };
        }

        [HttpPut("update")]
        public async Task<ApiResult<bool>> Update([FromBody] EditPaymentAccountRequest request)
        {
            var paymentAccount = await _unitOfWork.PaymentAccounts.GetByIdAsync(request.Id);

            if (paymentAccount == null)
            {
                return new ApiResult<bool>()
                {
                    Status = false,
                    Message = "Không tìm thấy PaymentAccount!",
                    Data = false
                };
            }
            _mapper.Map(request, paymentAccount);
            
            await _unitOfWork.PaymentAccounts.UpdateAsync(paymentAccount);


            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Cập nhật BankAccount thành công!",
                Data = true
            };

        }

        [HttpPut("delete")]
        public async Task<ApiResult<bool>> Delete([FromBody] EntityIdentityRequest<int> request)
        {
            var paymentAccount = await _unitOfWork.PaymentAccounts.GetByIdAsync(request.Id);

            if (paymentAccount == null)
            {
                return new ApiResult<bool>()
                {
                    Status = false,
                    Message = "Không tìm thấy BankAccount!",
                    Data = false
                };
            }

            await _unitOfWork.PaymentAccounts.DeleteAsync(paymentAccount);

            await _unitOfWork.CompleteAsync();

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Xoá PaymentAccount thành công!",
                Data = true,
            };
        }
    }
}
