namespace ProductsOrder_ASP.Net_API.Models.Dtos
{
    public class UserOrderDto
    {
        public Guid UserId { get; set; }
        public List<Orders> Orders { get; set; }
    }
}
