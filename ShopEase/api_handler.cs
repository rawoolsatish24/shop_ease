using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel.Communication;
using Newtonsoft.Json;

namespace ShopEase
{
    public class api_handler
    {
        int result = -1;
        string content;
        string content_type = "application/json";
        string parameters;
        HttpClient http_client = new();

        public async Task<int?> AddUser(User new_user)
        {
            try
            {
                result = -1;
                http_client = new HttpClient();
                http_client.BaseAddress = new Uri("https://localhost:7223/api/User/AddUser");
                http_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(content_type));
                http_client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", content_type);
                StringContent content = new(JsonConvert.SerializeObject(new_user), Encoding.UTF8, content_type);
                HttpResponseMessage response = await http_client.PostAsync("", content);
                if (response.IsSuccessStatusCode) { result = int.Parse(response.Content.ReadAsStringAsync().Result); }
                return result;
            }
            catch { return null; }
        }

        public async Task<int?> AddProduct(Product new_product)
        {
            try
            {
                result = -1;
                http_client = new HttpClient();
                http_client.BaseAddress = new Uri("https://localhost:7223/api/Product/AddProduct");
                http_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(content_type));
                http_client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", content_type);
                StringContent content = new(JsonConvert.SerializeObject(new_product), Encoding.UTF8, content_type);
                HttpResponseMessage response = await http_client.PostAsync("", content);
                if (response.IsSuccessStatusCode) { result = int.Parse(response.Content.ReadAsStringAsync().Result); }
                return result;
            }
            catch { return null; }
        }

        public async Task<int?> AddOrder(Order new_order)
        {
            try
            {
                result = -1;
                http_client = new HttpClient();
                http_client.BaseAddress = new Uri("https://localhost:7223/api/Order/AddOrder");
                http_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(content_type));
                http_client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", content_type);
                StringContent content = new(JsonConvert.SerializeObject(new_order), Encoding.UTF8, content_type);
                HttpResponseMessage response = await http_client.PostAsync("", content);
                if (response.IsSuccessStatusCode) { result = int.Parse(response.Content.ReadAsStringAsync().Result); }
                return result;
            }
            catch { return null; }
        }

        public async Task<int?> UpdateUser(User edited_user)
        {
            try
            {
                result = -1;
                http_client = new HttpClient();
                http_client.BaseAddress = new Uri("https://localhost:7223/api/User/UpdateUser");
                http_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(content_type));
                http_client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", content_type);
                StringContent content = new(JsonConvert.SerializeObject(edited_user), Encoding.UTF8, content_type);
                HttpResponseMessage response = await http_client.PutAsync("", content);
                if (response.IsSuccessStatusCode) { result = int.Parse(response.Content.ReadAsStringAsync().Result); }
                return result;
            }
            catch { return null; }
        }

        public async Task<int?> UpdateProduct(Product edited_product)
        {
            try
            {
                result = -1;
                http_client = new HttpClient();
                http_client.BaseAddress = new Uri("https://localhost:7223/api/Product/UpdateProduct");
                http_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(content_type));
                http_client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", content_type);
                StringContent content = new(JsonConvert.SerializeObject(edited_product), Encoding.UTF8, content_type);
                HttpResponseMessage response = await http_client.PutAsync("", content);
                if (response.IsSuccessStatusCode) { result = int.Parse(response.Content.ReadAsStringAsync().Result); }
                return result;
            }
            catch { return null; }
        }

        public async Task<int?> UpdateOrder(Order edited_order)
        {
            try
            {
                result = -1;
                http_client = new HttpClient();
                http_client.BaseAddress = new Uri("https://localhost:7223/api/Order/UpdateOrder");
                http_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(content_type));
                http_client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", content_type);
                StringContent content = new(JsonConvert.SerializeObject(edited_order), Encoding.UTF8, content_type);
                HttpResponseMessage response = await http_client.PutAsync("", content);
                if (response.IsSuccessStatusCode) { result = int.Parse(response.Content.ReadAsStringAsync().Result); }
                return result;
            }
            catch { return null; }
        }

        public async Task<User?> CheckUserLogin(string email_id, string password)
        {
            try
            {
                http_client = new HttpClient();
                http_client.BaseAddress = new Uri("https://localhost:7223/api/User/CheckUserLogin");
                parameters = $"?email_id={email_id}&password={password}";
                HttpResponseMessage response = await http_client.GetAsync(parameters);
                if (response.IsSuccessStatusCode)
                {
                    content = response.Content.ReadAsStringAsync().Result;
                    return await Task.FromResult(JsonConvert.DeserializeObject<User>(content));
                }
                else { return null; }
            }
            catch { return null; }
        }

        public async Task<User?> SearchUser(String user_id)
        {
            try
            {
                http_client = new HttpClient();
                http_client.BaseAddress = new Uri("https://localhost:7223/api/User/SearchUser");
                parameters = $"?user_id={user_id}";
                HttpResponseMessage response = await http_client.GetAsync(parameters);
                if (response.IsSuccessStatusCode)
                {
                    content = response.Content.ReadAsStringAsync().Result;
                    return await Task.FromResult(JsonConvert.DeserializeObject<User>(content));
                }
                else { return null; }
            }
            catch { return null; }
        }

        public async Task<IEnumerable<Product>?> GetRetailerProducts(String retailer_id)
        {
            try
            {
                http_client = new HttpClient();
                http_client.BaseAddress = new Uri("https://localhost:7223/api/Product/GetRetailerProducts");
                parameters = $"?retailer_id={retailer_id}";
                HttpResponseMessage response = await http_client.GetAsync(parameters);
                if (response.IsSuccessStatusCode)
                {
                    content = response.Content.ReadAsStringAsync().Result;
                    return await Task.FromResult(JsonConvert.DeserializeObject<IEnumerable<Product>>(content));
                }
                else { return null; }
            }
            catch { return null; }
        }

        public async Task<IEnumerable<Order>?> GetConsumerOrders(String consumer_id)
        {
            try
            {
                http_client = new HttpClient();
                http_client.BaseAddress = new Uri("https://localhost:7223/api/Order/GetConsumerOrders");
                parameters = $"?consumer_id={consumer_id}";
                HttpResponseMessage response = await http_client.GetAsync(parameters);
                if (response.IsSuccessStatusCode)
                {
                    content = response.Content.ReadAsStringAsync().Result;
                    return await Task.FromResult(JsonConvert.DeserializeObject<IEnumerable<Order>>(content));
                }
                else { return null; }
            }
            catch { return null; }
        }

        public async Task<IEnumerable<Order>?> GetOrdersForRetailer(String retailer_id)
        {
            try
            {
                http_client = new HttpClient();
                http_client.BaseAddress = new Uri("https://localhost:7223/api/Order/GetOrdersForRetailer");
                parameters = $"?retailer_id={retailer_id}";
                HttpResponseMessage response = await http_client.GetAsync(parameters);
                if (response.IsSuccessStatusCode)
                {
                    content = response.Content.ReadAsStringAsync().Result;
                    return await Task.FromResult(JsonConvert.DeserializeObject<IEnumerable<Order>>(content));
                }
                else { return null; }
            }
            catch { return null; }
        }

        public async Task<IEnumerable<Product>?> GetAvailableProducts()
        {
            try
            {
                http_client = new HttpClient();
                http_client.BaseAddress = new Uri("https://localhost:7223/api/Product/GetAvailableProducts");
                parameters = "";
                HttpResponseMessage response = await http_client.GetAsync(parameters);
                if (response.IsSuccessStatusCode)
                {
                    content = response.Content.ReadAsStringAsync().Result;
                    return await Task.FromResult(JsonConvert.DeserializeObject<IEnumerable<Product>>(content));
                }
                else { return null; }
            }
            catch { return null; }
        }

        public async Task<int?> GetConsumerUniqueItemsCount(String consumer_id)
        {
            try
            {
                http_client = new HttpClient();
                http_client.BaseAddress = new Uri("https://localhost:7223/api/Counter/GetConsumerUniqueItemsCount");
                parameters = $"?consumer_id={consumer_id}";
                HttpResponseMessage response = await http_client.GetAsync(parameters);
                if (response.IsSuccessStatusCode) { result = int.Parse(response.Content.ReadAsStringAsync().Result); }
                return result;
            }
            catch { return null; }
        }

        public async Task<int?> GetConsumerTotalOrdersCount(String consumer_id)
        {
            try
            {
                http_client = new HttpClient();
                http_client.BaseAddress = new Uri("https://localhost:7223/api/Counter/GetConsumerTotalOrdersCount");
                parameters = $"?consumer_id={consumer_id}";
                HttpResponseMessage response = await http_client.GetAsync(parameters);
                if (response.IsSuccessStatusCode) { result = int.Parse(response.Content.ReadAsStringAsync().Result); }
                return result;
            }
            catch { return null; }
        }

        public async Task<int?> GetConsumerTotalPurchaseAmount(String consumer_id)
        {
            try
            {
                http_client = new HttpClient();
                http_client.BaseAddress = new Uri("https://localhost:7223/api/Counter/GetConsumerTotalPurchaseAmount");
                parameters = $"?consumer_id={consumer_id}";
                HttpResponseMessage response = await http_client.GetAsync(parameters);
                if (response.IsSuccessStatusCode) { result = int.Parse(response.Content.ReadAsStringAsync().Result); }
                return result;
            }
            catch { return null; }
        }

        public async Task<int?> GetRetailerProductsCount(String retailer_id)
        {
            try
            {
                http_client = new HttpClient();
                http_client.BaseAddress = new Uri("https://localhost:7223/api/Counter/GetRetailerProductsCount");
                parameters = $"?retailer_id={retailer_id}";
                HttpResponseMessage response = await http_client.GetAsync(parameters);
                if (response.IsSuccessStatusCode) { result = int.Parse(response.Content.ReadAsStringAsync().Result); }
                return result;
            }
            catch { return null; }
        }

        public async Task<int?> GetRetailerOrdersCount(String retailer_id)
        {
            try
            {
                http_client = new HttpClient();
                http_client.BaseAddress = new Uri("https://localhost:7223/api/Counter/GetRetailerOrdersCount");
                parameters = $"?retailer_id={retailer_id}";
                HttpResponseMessage response = await http_client.GetAsync(parameters);
                if (response.IsSuccessStatusCode) { result = int.Parse(response.Content.ReadAsStringAsync().Result); }
                return result;
            }
            catch { return null; }
        }

        public async Task<int?> GetRetailerPendingOrdersCount(String retailer_id)
        {
            try
            {
                http_client = new HttpClient();
                http_client.BaseAddress = new Uri("https://localhost:7223/api/Counter/GetRetailerPendingOrdersCount");
                parameters = $"?retailer_id={retailer_id}";
                HttpResponseMessage response = await http_client.GetAsync(parameters);
                if (response.IsSuccessStatusCode) { result = int.Parse(response.Content.ReadAsStringAsync().Result); }
                return result;
            }
            catch { return null; }
        }

        public async Task<int?> GetRetailerTotalSales(String retailer_id)
        {
            try
            {
                http_client = new HttpClient();
                http_client.BaseAddress = new Uri("https://localhost:7223/api/Counter/GetRetailerTotalSales");
                parameters = $"?retailer_id={retailer_id}";
                HttpResponseMessage response = await http_client.GetAsync(parameters);
                if (response.IsSuccessStatusCode) { result = int.Parse(response.Content.ReadAsStringAsync().Result); }
                return result;
            }
            catch { return null; }
        }
    }
}
