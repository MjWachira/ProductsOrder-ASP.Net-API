namespace ProductsOrder_ASP.Net_API.Models.Dtos
{
    public class UserOrderWithProductsDto
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Products> Products { get; set; }
    }
}
