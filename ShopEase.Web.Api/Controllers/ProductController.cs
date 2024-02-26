using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ShopEase.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        db_handler db_handler = new db_handler();

        [HttpPost("AddProduct")]
        public async Task<int> AddProduct(Product new_product)
        {
            try
            {
                ProductViewModel new_product_view_model = new ProductViewModel
                {
                    RetailerId = new_product.RetailerId,
                    RetailerName = new_product.RetailerName,
                    Name = new_product.Name,
                    Description = new_product.Description,
                    Price = new_product.Price,
                    Stock = new_product.Stock
                };

                int product_counts = await db_handler.ScalarQueryAsync("SELECT COUNT(*) FROM ProductViewModel WHERE Name=? AND RetailerId=?", new object[] { new_product_view_model.Name, new_product_view_model.RetailerId });
                if (product_counts > 0) { return -2; }

                return await db_handler.AddAsync(new_product_view_model);
            }
            catch { }
            return -1;
        }

        [HttpPut("UpdateProduct")]
        public async Task<int> UpdateProduct(Product edited_product)
        {
            try
            {
                ProductViewModel edited_product_view_model = new ProductViewModel
                {
                    Id = edited_product.Id,
                    RetailerId = edited_product.RetailerId,
                    RetailerName = edited_product.RetailerName,
                    Name = edited_product.Name,
                    Description = edited_product.Description,
                    Price = edited_product.Price,
                    Stock = edited_product.Stock,
                    CreationDate = edited_product.CreationDate
                };

                int product_counts = await db_handler.ScalarQueryAsync("SELECT COUNT(*) FROM ProductViewModel WHERE Name=? AND RetailerId=? AND Id<>?", new object[] { edited_product.Name, edited_product.RetailerId, edited_product.Id });
                if (product_counts > 0) { return -2; }

                return await db_handler.UpdateAsync(edited_product_view_model);
            }
            catch { }
            return -1;
        }

        [HttpGet("GetRetailerProducts")]
        public async Task<IEnumerable<Product>> GetRetailerProducts(String retailer_id)
        {
            List<Product> retailer_products_list = new List<Product>();
            try
            {
                IEnumerable<ProductViewModel> result = await db_handler.ProductQueryAsync("SELECT * FROM ProductViewModel WHERE RetailerId=? ORDER BY CreationDate DESC", new object[] { retailer_id });
                if (result != null)
                {
                    foreach (ProductViewModel product in result)
                    {
                        retailer_products_list.Add(new Product
                        {
                            Id = product.Id,
                            RetailerId = product.RetailerId,
                            RetailerName = product.RetailerName,
                            Name = product.Name,
                            Description = product.Description,
                            Price = product.Price,
                            Stock = product.Stock,
                            CreationDate = product.CreationDate
                        });
                    }
                }
            }
            catch {}
            return retailer_products_list;
        }

        [HttpGet("GetAvailableProducts")]
        public async Task<IEnumerable<Product>> GetAvailableProducts()
        {
            List<Product> available_products_list = new List<Product>();
            try
            {
                IEnumerable<ProductViewModel> result = await db_handler.ProductQueryAsync("SELECT * FROM ProductViewModel WHERE Stock>0 ORDER BY Id DESC", Array.Empty<object>());
                if (result != null)
                {
                    foreach (ProductViewModel product in result)
                    {
                        available_products_list.Add(new Product
                        {
                            Id = product.Id,
                            RetailerId = product.RetailerId,
                            RetailerName = product.RetailerName,
                            Name = product.Name,
                            Description = product.Description,
                            Price = product.Price,
                            Stock = product.Stock,
                            CreationDate = product.CreationDate
                        });
                    }
                }
            }
            catch { }
            return available_products_list;
        }
    }
}
