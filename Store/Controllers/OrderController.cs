using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Store.Core;
using Store.Core.Dtos.Orders;
using Store.Core.Entities.Order;
using Store.Core.Services.Contract;
using System.Security.Claims;

namespace Store.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IOrderService orderService , IMapper mapper, IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder(OrderDto model)
        {
            if (model is null)
            {
                return BadRequest("Order model cannot be null.");
            }
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null) return Unauthorized("Please SignUp!");

            var Address = _mapper.Map<Address>(model.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(userEmail, model.basketId, model.DeliveryMethod, Address);
            if (order is null) return BadRequest("Failed to create order.");

            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrderForSpecificUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null) return Unauthorized();
            var orders = await _orderService.GetOrderForSpecificUserAsync(userEmail);
            if(orders is null) return BadRequest();
             
            var ordersMapped = _mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
            return Ok(ordersMapped);
        }


        [Authorize]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderByIdForSpecificUser(int orderId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null) return Unauthorized();
            var order = await _orderService.GetOrderByIdForSpecificUserAsync(userEmail, orderId);
            if (order is null) return NotFound();
            var orderMapped = _mapper.Map<OrderToReturnDto>(order);

            return Ok(orderMapped);
        }

        [Authorize]
        [HttpGet("DeliveryMethods")]
        public async Task<IActionResult> GetDeliveryMethod()
        {
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod,int>().GetAllAsync();
            if(deliveryMethod is null) return BadRequest();

            return Ok(deliveryMethod);
        }
    }
}