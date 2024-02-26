namespace ShopEase
{
    public class Product
    {
        public int Id { get; set; }

        public required int RetailerId { get; set; }

        public required string RetailerName { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public required int Price { get; set; }

        public required int Stock { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
