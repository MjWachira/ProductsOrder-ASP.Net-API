using Microsoft.EntityFrameworkCore;
using ProductsOrder_ASP.Net_API.Data;
using ProductsOrder_ASP.Net_API.Models;
using ProductsOrder_ASP.Net_API.Models.Dtos;
using ProductsOrder_ASP.Net_API.Services.IServices;

namespace ProductsOrder_ASP.Net_API.Services
{
    public class ProductsServices : IProduct
    {
        private readonly ApplicationDBContext _context;
        public ProductsServices(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<string> AddProduct(Products Product)
        {
            await _context.Products.AddAsync(Product);
            await _context.SaveChangesAsync();

            return "Product Added Successfully";

        }

        public async Task<bool> DeleteProduct(Products product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<List<Products>> GetPagedProducts(int pageNumber, int pageSize)
        {
            
            int skip = (pageNumber - 1) * pageSize;

            // Retrieve products based on pagination
            var pagedProducts = await _context.Products
                                                .OrderBy(p => p.Id) 
                                                .Skip(skip)
                                                .Take(pageSize)
                                                .ToListAsync();

            return pagedProducts;
        }


        public Task<Products> GetOneProduct(Guid Id)
        {
            return _context.Products.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateProduct(Products Product)
        {
            _context.Products.Update(Product);
            await _context.SaveChangesAsync();

            return "Product Added Successfully";
        }
        public async Task<List<Products>> FilterProducts(string productName, string? price)
        {
            IQueryable<Products> query = _context.Products;

            // Filter by product name
            query = query.Where(p => p.ProductName.Contains(productName));

            // Filter by price if provided
            if (price != null)
            {
                query = query.Where(p => p.ProductCost.Contains(price));
            }

            return await query.ToListAsync();
        }




    }
}
