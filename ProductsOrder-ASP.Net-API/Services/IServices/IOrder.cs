using ProductsOrder_ASP.Net_API.Models;
using ProductsOrder_ASP.Net_API.Models.Dtos;

namespace ProductsOrder_ASP.Net_API.Services.IServices
{
    public interface IOrder
    {
        Task<List<Orders>> GetAllOrders();
        Task<Orders> GetOneOrder(Guid id);
        Task<string> AddOrder(Orders order);
        Task<string> UpdateOrder(Orders order);
        Task<bool> DeleteOrder(Orders order);
    }
}
