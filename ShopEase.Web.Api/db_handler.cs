using Microsoft.VisualBasic.FileIO;
using SQLite;

namespace ShopEase.Web.Api
{
    public class db_handler
    {
        private const string name = "ShopEaseDB.db3";
        private static string file_path => Path.Combine(FileSystem.CurrentDirectory, name);

        public SQLiteAsyncConnection sqlite_conn;

        public db_handler()
        {
            sqlite_conn = new SQLiteAsyncConnection(file_path);
            sqlite_conn.CreateTableAsync<UserViewModel>().Wait();
            sqlite_conn.CreateTableAsync<ProductViewModel>().Wait();
            sqlite_conn.CreateTableAsync<OrderViewModel>().Wait();
        }

        public async Task<int> AddAsync(UserViewModel new_user)
        {
            try
            {
                await sqlite_conn.InsertAsync(new_user);
                return new_user.Id;
            } catch { return -1; }
        }

        public async Task<int> AddAsync(OrderViewModel new_order)
        {
            try
            {
                await sqlite_conn.InsertAsync(new_order);
                return new_order.Id;
            }
            catch { return -1; }
        }

        public async Task<int> AddAsync(ProductViewModel new_product)
        {
            try
            {
                await sqlite_conn.InsertAsync(new_product);
                return new_product.Id;
            }
            catch { return -1; }
        }

        public async Task<int> ScalarQueryAsync(string query, object[] args)
        {
            return await sqlite_conn.ExecuteScalarAsync<int>(query, args);
        }

        public async Task<IEnumerable<UserViewModel>> GetAsync()
        {
            return await Task.FromResult(await sqlite_conn.Table<UserViewModel>().ToListAsync());
        }

        public async Task<int> DeleteAsync() { return await sqlite_conn.DeleteAllAsync<UserViewModel>(); }

        public async Task<int> GetCountAsync()
        {
            try { return await sqlite_conn.ExecuteScalarAsync<int>("SELECT COUNT(*) AS NO_OF_RECORDS FROM UserViewModel", Array.Empty<object>()); }
            catch { return -1; }
        }

        public async Task<int> UpdateAsync(UserViewModel edit_user)
        {
            try
            {
                return await sqlite_conn.UpdateAsync(edit_user);
            }
            catch { return -1; }
        }

        public async Task<int> UpdateAsync(OrderViewModel edit_order)
        {
            try
            {
                return await sqlite_conn.UpdateAsync(edit_order);
            }
            catch { return -1; }
        }

        public async Task<int> UpdateAsync(ProductViewModel edit_product)
        {
            try
            {
                return await sqlite_conn.UpdateAsync(edit_product);
            }
            catch { return -1; }
        }

        public async Task<UserViewModel> FindWithQueryAsync(string query, object[] args)
        {
            try
            {
                return await sqlite_conn.FindWithQueryAsync<UserViewModel>(query, args);
            }
            catch { return null; }
        }

        public async Task<IEnumerable<ProductViewModel>> ProductQueryAsync(string query, object[] args)
        {
            try
            {
                return await sqlite_conn.QueryAsync<ProductViewModel>(query, args);
            }
            catch { return null; }
        }

        public async Task<IEnumerable<OrderViewModel>> OrderQueryAsync(string query, object[] args)
        {
            try
            {
                return await sqlite_conn.QueryAsync<OrderViewModel>(query, args);
            }
            catch { return null; }
        }
    }
}
