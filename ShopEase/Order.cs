namespace ShopEase
{
    public class Order
    {
        public int Id { get; set; }

        public required int ConsumerId { get; set; }

        public required int ProductId { get; set; }

        public required string ProductName { get; set; }

        public required string ProductDescription { get; set; }

        public required int Quantity { get; set; }

        public required int Price { get; set; }

        public required int TotalAmount { get; set; }

        public required string Address { get; set; }

        public string? Status { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
