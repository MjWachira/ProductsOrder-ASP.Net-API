using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsOrder_ASP.Net_API.Models.Dtos
{
    public class AddOrderDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid ProductId { get; set; }
    }
}
