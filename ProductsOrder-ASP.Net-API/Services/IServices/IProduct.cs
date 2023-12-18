using ProductsOrder_ASP.Net_API.Models;
using ProductsOrder_ASP.Net_API.Models.Dtos;

namespace ProductsOrder_ASP.Net_API.Services.IServices
{
    public interface IProduct
    {
        //Task<List<Products>> GetAllProducts();

        Task<List<Products>> GetPagedProducts(int pageNumber, int pageSize);
        Task<Products> GetOneProduct(Guid id);
        Task<string> AddProduct(Products product);
        Task<string> UpdateProduct(Products product);
        Task<bool> DeleteProduct(Products product);
        Task<List<Products>> FilterProducts(string productName, string? price);

    }
}
