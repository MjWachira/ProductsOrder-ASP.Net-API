using Microsoft.EntityFrameworkCore;
using ProductsOrder_ASP.Net_API.Data;
using ProductsOrder_ASP.Net_API.Models;
using ProductsOrder_ASP.Net_API.Services.IServices;

namespace ProductsOrder_ASP.Net_API.Services
{
    public class OrdersService : IOrder
    {

        private readonly ApplicationDBContext _context;
        public OrdersService(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<string> AddOrder(Orders Order)
        {

            await _context.Orders.AddAsync(Order);
            await _context.SaveChangesAsync();

            return "Order Added Successfully";

        }

        public async Task<bool> DeleteOrder(Orders Order)
        {
            _context.Orders.Remove(Order);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Orders>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public  Task<Orders> GetOneOrder(Guid Id)
        {
            return _context.Orders.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateOrder(Orders Order)
        {
            _context.Orders.Update(Order);
            await _context.SaveChangesAsync();

            return "Order Added Successfully";
        }
    }
}
