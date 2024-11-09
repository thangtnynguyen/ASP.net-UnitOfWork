using AFF_BE.Core.ISeedWorks;
using AFF_BE.Core.Models.Common;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AFF_BE.Api.Controllers.Commission
{
    [Route("api/commission")]
    [ApiController]
    public class CommissionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CommissionController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("calculate-commission")]
        public async Task<ApiResult<bool>> CalculateCommission([FromQuery] int orderId)
        {
            await _unitOfWork.Commissions.CalculateCommissionAsync(orderId);
            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Chia hoa hồng thành công!",
                Data = true
            };
        }
    }
}
