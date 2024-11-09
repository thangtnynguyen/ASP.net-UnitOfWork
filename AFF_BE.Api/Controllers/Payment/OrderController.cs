using AFF_BE.Core.Constants.System;
using AFF_BE.Core.Data.Payment;
using AFF_BE.Core.Helpers;
using AFF_BE.Core.ISeedWorks;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Payment;
using AFF_BE.Core.Models.Payment.Order;
using AFF_BE.Data;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AFF_BE.Api.Controllers.Payment
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
         [HttpGet("paging")]
        public async Task<ApiResult<PagingResult<OrderDto>>> Get([FromQuery] GetOrderRequest request)
        {
            var result = await _unitOfWork.Orders.GetAllPaging(request.OrderTrackingNumber,request.PaymentAccountReceiptId,request.OrderStatus,request.UserName,
                request.FromDate,request.ToDate,request.SortBy, request.OrderBy,request.PageIndex,request.PageSize);

            return new ApiResult<PagingResult<OrderDto>>()
            {
                Status = true,
                Message = "Danh sách Đơn Hàng đã được lấy thành công!",
                Data = result
            };
        }
        [HttpPost("create")]
        public async Task<ApiResult<bool>> Create([FromBody] CreateOrderRequest request)
        {
            var trackingNumber = StringHelper.GenerateTrackingNumberFormat(5,request.UserId.ToString());
            var order = _mapper.Map<CreateOrderRequest, Order>(request);
            
            order.OrderTrackingNumber = trackingNumber;
            await _unitOfWork.Orders.CreateAsync(order);
            
            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Tạo mới Đơn hàng thành công!",
                Data = true
            };
        }

        [HttpPut("update-status")]
        public async Task<ApiResult<bool>> Update([FromBody] EditOrderStatus request)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(request.Id);
            order.OrderStatus = request.OrderStatus;
            
            await _unitOfWork.Orders.UpdateAsync(order);
            
            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Cập nhật trạng thái đơn hàng thành công!",
                Data = true
            };
        
        }

        
    }
}
