using AFF_BE.Core.ISeedWorks;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Payment;

namespace AFF_BE.Core.IRepositories;

public interface IPaymentAccountRepository:IRepositoryBase<PaymentAccount, int>
{
    Task<PagingResult<PaymentAccountDto>> GetAllPaging(string? bankCode, string? bankName, string? sortBy, string? orderBy, int pageIndex = 1, int pageSize = 10);

}