using AFF_BE.Core.Data.Payment;
using AFF_BE.Core.ISeedWorks;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Payment.Order;

namespace AFF_BE.Core.IRepositories;

public interface IOrderRepository:IRepositoryBase<Order,int>
{
    Task<PagingResult<OrderDto>> GetAllPaging(string? orderTrackingNumber,int? paymentAccountReceipt,OrderStatus? orderStatus , string? userName, DateTime? fromDate,DateTime? toDate, string? sortBy, string? orderBy, int pageIndex = 1, int pageSize = 10);

}