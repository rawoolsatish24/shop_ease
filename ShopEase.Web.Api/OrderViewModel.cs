using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopEase.Web.Api
{
    public class OrderViewModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey("ConsumerId")]
        public int ConsumerId { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public int TotalAmount { get; set; }

        public string? Address { get; set; }

        public string Status { get; set; } = "ORDER PLACED";

        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
