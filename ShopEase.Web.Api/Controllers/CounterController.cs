using Microsoft.AspNetCore.Mvc;

namespace ShopEase.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounterController : ControllerBase
    {
        db_handler db_handler = new db_handler();

        [HttpGet("GetRetailerProductsCount")]
        public async Task<int> GetRetailerProductsCount(String retailer_id)
        {
            try
            {
                return await db_handler.ScalarQueryAsync("SELECT COUNT(Id) AS ResultCount FROM ProductViewModel WHERE RetailerId=?", new object[] { retailer_id });
            }
            catch { }
            return -1;
        }

        [HttpGet("GetConsumerUniqueItemsCount")]
        public async Task<int> GetConsumerUniqueItemsCount(String consumer_id)
        {
            try
            {
                return await db_handler.ScalarQueryAsync("SELECT COUNT(DISTINCT ProductId) AS ResultCount FROM OrderViewModel WHERE ConsumerId=?", new object[] { consumer_id });
            }
            catch { }
            return -1;
        }

        [HttpGet("GetConsumerTotalOrdersCount")]
        public async Task<int> GetConsumerTotalOrdersCount(String consumer_id)
        {
            try
            {
                return await db_handler.ScalarQueryAsync("SELECT COUNT(Id) AS ResultCount FROM OrderViewModel WHERE ConsumerId=?", new object[] { consumer_id });
            }
            catch { }
            return -1;
        }

        [HttpGet("GetConsumerTotalPurchaseAmount")]
        public async Task<int> GetConsumerTotalPurchaseAmount(String consumer_id)
        {
            try
            {
                return await db_handler.ScalarQueryAsync("SELECT SUM(TotalAmount) AS TotalPurchaseAmount FROM OrderViewModel WHERE ConsumerId=? AND Status NOT IN ('CANCELED', 'REJECTED')", new object[] { consumer_id });
            }
            catch { }
            return -1;
        }

        [HttpGet("GetRetailerOrdersCount")]
        public async Task<int> GetRetailerOrdersCount(String retailer_id)
        {
            try
            {
                return await db_handler.ScalarQueryAsync("SELECT COUNT(Id) AS ResultCount FROM OrderViewModel WHERE ProductId IN (SELECT Id FROM ProductViewModel WHERE RetailerId=?)", new object[] { retailer_id });
            }
            catch { }
            return -1;
        }

        [HttpGet("GetRetailerPendingOrdersCount")]
        public async Task<int> GetRetailerPendingOrdersCount(String retailer_id)
        {
            try
            {
                return await db_handler.ScalarQueryAsync("SELECT COUNT(Id) AS ResultCount FROM OrderViewModel WHERE ProductId IN (SELECT Id FROM ProductViewModel WHERE RetailerId=?) AND Status NOT IN ('DELIVERED', 'REJECTED', 'CANCELED')", new object[] { retailer_id });
            }
            catch { }
            return -1;
        }

        [HttpGet("GetRetailerTotalSales")]
        public async Task<int> GetRetailerTotalSales(String retailer_id)
        {
            try
            {
                return await db_handler.ScalarQueryAsync("SELECT SUM(TotalAmount) AS TotalSales FROM OrderViewModel WHERE ProductId IN (SELECT Id FROM ProductViewModel WHERE RetailerId=?) AND Status NOT IN ('CANCELED', 'REJECTED')", new object[] { retailer_id });
            }
            catch { }
            return -1;
        }
    }
}
