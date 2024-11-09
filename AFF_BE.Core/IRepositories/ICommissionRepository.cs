using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AFF_BE.Core.Models.Commission;
using AFF_BE.Core.Models.Common;

namespace AFF_BE.Core.IRepositories
{
    public interface ICommissionRepository
    {
        //Task CalculateCommissionAsync(int orderId);
        Task<GetCommissionByUserDto> GetCommissionByUserId(int userId);
        Task CalculateCommissionAsync(int orderId);
    }
}
