using ProductsOrder_ASP.Net_API.Services.IServices;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsOrder_ASP.Net_API.Models
{
    public class Orders
    {
        public Guid Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
               
        public Guid UserId { get; set; }
        public Users User { get; set; }

       
        public List<Products> Products { get; set; } = new List<Products>();


    }
}
