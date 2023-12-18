using ProductsOrder_ASP.Net_API.Models;

namespace ProductsOrder_ASP.Net_API.Services.IServices
{
    public interface IJwt
    {
        string GenerateToken(Users user);
    }
}
