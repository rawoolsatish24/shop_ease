using Microsoft.AspNetCore.Mvc;

namespace ShopEase.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        db_handler db_handler = new db_handler();

        [HttpPost("AddOrder")]
        public async Task<int> AddOrder(Order new_order)
        {
            try
            {
                int new_order_id = await db_handler.AddAsync(new OrderViewModel
                {
                    ConsumerId = new_order.ConsumerId,
                    ProductId = new_order.ProductId,
                    ProductName = new_order.ProductName,
                    ProductDescription = new_order.ProductDescription,
                    Quantity = new_order.Quantity,
                    Price = new_order.Price,
                    TotalAmount = new_order.TotalAmount,
                    Address = new_order.Address
                });

                await db_handler.ScalarQueryAsync("UPDATE ProductViewModel SET Stock=(Stock-" + new_order.Quantity.ToString() + ") WHERE Id=?", new object[] { new_order.ProductId });
                return new_order_id;
            }
            catch { }
            return -1;
        }

        [HttpPut("UpdateOrder")]
        public async Task<int> UpdateOrder(Order edited_order)
        {
            try
            {
                return await db_handler.UpdateAsync(new OrderViewModel
                {
                    Id = edited_order.Id,
                    ConsumerId = edited_order.ConsumerId,
                    ProductId = edited_order.ProductId,
                    ProductName = edited_order.ProductName,
                    ProductDescription = edited_order.ProductDescription,
                    Quantity = edited_order.Quantity,
                    Price = edited_order.Price,
                    TotalAmount = edited_order.TotalAmount,
                    Address = edited_order.Address,
                    Status = edited_order.Status,
                    OrderDate = edited_order.OrderDate
                });
            }
            catch { }
            return -1;
        }

        [HttpGet("GetConsumerOrders")]
        public async Task<IEnumerable<Order>> GetConsumerOrders(String consumer_id)
        {
            List<Order> consumer_orders_list = new List<Order>();
            try
            {
                IEnumerable<OrderViewModel> result = await db_handler.OrderQueryAsync("SELECT * FROM OrderViewModel WHERE ConsumerId=? ORDER BY OrderDate DESC", new object[] { consumer_id });
                if (result != null)
                {
                    foreach (OrderViewModel order in result)
                    {
                        consumer_orders_list.Add(new Order
                        {
                            Id = order.Id,
                            ConsumerId = order.ConsumerId,
                            ProductId = order.ProductId,
                            ProductName = order.ProductName,
                            ProductDescription = order.ProductDescription,
                            Quantity = order.Quantity,
                            Price = order.Price,
                            TotalAmount = order.TotalAmount,
                            Address = order.Address,
                            Status = order.Status,
                            OrderDate = order.OrderDate
                        });
                    }
                }
            }
            catch { }
            return consumer_orders_list;
        }

        [HttpGet("GetOrdersForRetailer")]
        public async Task<IEnumerable<Order>> GetOrdersForRetailer(String retailer_id)
        {
            List<Order> retailer_orders_list = new List<Order>();
            try
            {
                IEnumerable<OrderViewModel> result = await db_handler.OrderQueryAsync("SELECT * FROM OrderViewModel WHERE ProductId IN (SELECT Id FROM ProductViewModel WHERE RetailerId=?) ORDER BY OrderDate DESC", new object[] { retailer_id });
                if (result != null)
                {
                    foreach (OrderViewModel order in result)
                    {
                        retailer_orders_list.Add(new Order
                        {
                            Id = order.Id,
                            ConsumerId = order.ConsumerId,
                            ProductId = order.ProductId,
                            ProductName = order.ProductName,
                            ProductDescription = order.ProductDescription,
                            Quantity = order.Quantity,
                            Price = order.Price,
                            TotalAmount = order.TotalAmount,
                            Address = order.Address,
                            Status = order.Status,
                            OrderDate = order.OrderDate
                        });
                    }
                }
            }
            catch { }
            return retailer_orders_list;
        }
    }
}
