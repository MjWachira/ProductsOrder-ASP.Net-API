using ProductsOrder_ASP.Net_API.Services.IServices;

namespace ProductsOrder_ASP.Net_API.Models
{
    public class Products
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCost { get; set; }

        // Navigation property for orders 
        public List<Orders> Orders { get; set; } = new List<Orders>();
    }
}
