using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopEase.Web.Api
{
    public class ProductViewModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey("RetailerId")]
        public int RetailerId { get; set; }

        public string? RetailerName { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Price { get; set; }

        public int Stock { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
