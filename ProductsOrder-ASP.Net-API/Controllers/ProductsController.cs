using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsOrder_ASP.Net_API.Models;
using ProductsOrder_ASP.Net_API.Models.Dtos;
using ProductsOrder_ASP.Net_API.Services.IServices;

namespace ProductsOrder_ASP.Net_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IProduct _productservice;
        public readonly IMapper _mapper;
        public ProductsController(IProduct products, IMapper mapper) 
        {
            _productservice = products;
            _mapper = mapper;  
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Products>>> GetAllProducts(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                // Validate and sanitize page number and page size if needed
                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                if (pageSize < 1)
                {
                    pageSize = 10; // Default page size
                }

                var products = await _productservice.GetPagedProducts(pageNumber, pageSize);

                return Ok(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public  async Task<ActionResult<string>>AddProduct(AddProductDto Product)
        {
            var newProduct = _mapper.Map<Products>(Product);
            var response = await _productservice.AddProduct(newProduct);
            return Created($"Products/{newProduct.Id}",response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>>GetOneProduct(Guid id)
        {
            var product = await _productservice.GetOneProduct(id);
            if(product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<string>> DeleteProduct(Guid id)
        {
            var product = await _productservice.GetOneProduct(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            var response = await _productservice.DeleteProduct(product);
            return Ok(response);
        }
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<string>> UpdateProduct(AddProductDto uProduct , Guid id)
        {
            var product = await _productservice.GetOneProduct(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            var newProduct = _mapper.Map<Products>(uProduct);
            var response = await _productservice.UpdateProduct(newProduct);

            return Ok(response);

        }
        [HttpGet("filter")]
        [Authorize]
        public async Task<ActionResult<List<Products>>> FilterProducts([FromQuery] string productName, [FromQuery] string? price = null)
        {
            try
            {
                // Validate and sanitize input parameters if needed
                if (string.IsNullOrEmpty(productName))
                {
                    return BadRequest("Product name is required for filtering.");
                }

                var filteredProducts = await _productservice.FilterProducts(productName, price);

                return Ok(filteredProducts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }



    }
}
