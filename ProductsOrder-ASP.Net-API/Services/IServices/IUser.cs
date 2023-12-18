using ProductsOrder_ASP.Net_API.Models.Dtos;
using ProductsOrder_ASP.Net_API.Models;

namespace ProductsOrder_ASP.Net_API.Services.IServices
{
    public interface IUser
    {

        Task<Users> GetUserByEmail(string email);
        Task<string> RegisterUser(Users user);

    }
}
