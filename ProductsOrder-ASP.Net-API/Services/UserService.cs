using Microsoft.EntityFrameworkCore;
using ProductsOrder_ASP.Net_API.Data;
using ProductsOrder_ASP.Net_API.Models;
using ProductsOrder_ASP.Net_API.Services.IServices;

namespace ProductsOrder_ASP.Net_API.Services
{
    public class UserService : IUser
    {
        public readonly ApplicationDBContext _context;
        public UserService(ApplicationDBContext context)
        {
            _context = context;   
        }

        public async Task<string> RegisterUser(Users user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return "user Added Successfully";
        }

        public async Task<Users> GetUserByEmail(string email)
        {
            return await _context.Users.Where(b => b.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
        }
    }
}
