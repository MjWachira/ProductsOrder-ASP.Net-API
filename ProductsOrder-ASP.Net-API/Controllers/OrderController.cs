using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsOrder_ASP.Net_API.Data;
using ProductsOrder_ASP.Net_API.Models;
using ProductsOrder_ASP.Net_API.Models.Dtos;
using ProductsOrder_ASP.Net_API.Services.IServices;

namespace ProductsOrder_ASP.Net_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrder _orderservice;
        private readonly IProduct _productservice;
        private readonly ApplicationDBContext _context;

        public OrderController( IMapper mapper , IOrder order , IProduct product, ApplicationDBContext dBContext)
        {
            _mapper = mapper;
            _orderservice = order;
            _productservice = product;
            _context = dBContext;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Orders>>> GetAllOrders()
        {
            var orders = await _orderservice.GetAllOrders();
            var list = User.Claims.ToList();
            var Id = list[1].Value;

            Console.WriteLine($"User ID: {Id}");

            return Ok(orders);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>> MakeOrder(AddOrderDto Order)
        {


            var existingProduct = await _productservice.GetOneProduct(Order.ProductId);

            if (existingProduct == null)
            {
                return BadRequest("Invalid ProductId. Product not found.");
            }

            var newOrder = _mapper.Map<Orders>(Order);
            var response = await _orderservice.AddOrder(newOrder);
            return Created($"", response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> GetOneOrder(Guid id)
        {
            var order = await _orderservice.GetOneOrder(id);
            if (order == null)
            {
                return NotFound("order id not found");
            }
            return Ok(order);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdateProduct(AddOrderDto uOrder, Guid id)
        {
            var existingOrder = await _orderservice.GetOneOrder(id);

            if (existingOrder == null)
            {
                return NotFound($"Order with id {id} not found.");
            }

          
            // Update existingOrder properties
            
            existingOrder.OrderDate = uOrder.OrderDate;

            var response = await _orderservice.UpdateOrder(existingOrder);
            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteOrder(Guid id)
        {
            var order = await _orderservice.GetOneOrder(id);
            if (order == null)
            {
                return NotFound("Order not found");
            }

            var response = await _orderservice.DeleteOrder(order);
            return Ok(response);
        }
        [HttpGet("user/{userId}/with-products")]
        public async Task<ActionResult<List<UserOrderWithProductsDto>>> GetUserOrdersWithProducts(Guid userId)
        {
            try
            {
                var userOrders = await _orderservice.GetUserOrdersWithProducts(userId);

                if (userOrders == null || userOrders.Count == 0)
                {
                    return NotFound("User has no orders.");
                }

                var userOrderDtos = _mapper.Map<List<UserOrderWithProductsDto>>(userOrders);
                return Ok(userOrderDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }


    }
}
