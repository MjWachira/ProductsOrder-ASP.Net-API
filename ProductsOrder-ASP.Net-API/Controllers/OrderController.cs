using AutoMapper;
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
        public async Task<ActionResult<List<Orders>>> GetAllOrders()
        {
            var orders = await _orderservice.GetAllOrders();

            return Ok(orders);
        }
        [HttpPost]
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

            // Check for concurrency here if needed

            // Update existingOrder properties
            existingOrder.CustomerId = uOrder.CustomerId;
            existingOrder.CustomerName = uOrder.CustomerName;
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


    }
}
