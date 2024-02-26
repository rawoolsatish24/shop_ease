using Microsoft.AspNetCore.Mvc;

namespace ShopEase.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        db_handler db_handler = new db_handler();

        [HttpPost("AddUser")]
        public async Task<int> AddUser(User new_user)
        {
            try
            {
                UserViewModel new_user_view_model = new UserViewModel
                {
                    Name = new_user.Name,
                    EmailId = new_user.EmailId,
                    Mobile = new_user.Mobile,
                    Address = new_user.Address,
                    Password = new_user.Password,
                    Type = new_user.Type,
                };

                int email_counts = await db_handler.ScalarQueryAsync("SELECT COUNT(*) FROM UserViewModel WHERE EmailId=?", new object[] { new_user_view_model.EmailId });
                if (email_counts > 0 ) { return -2; }

                return await db_handler.AddAsync(new_user_view_model);
            } catch { }
            return -1;
        }

        [HttpPut("UpdateUser")]
        public async Task<int> UpdateUser(User edited_user)
        {
            try
            {
                UserViewModel edited_user_view_model = new UserViewModel
                {
                    Id = edited_user.Id,
                    Name = edited_user.Name,
                    EmailId = edited_user.EmailId,
                    Mobile = edited_user.Mobile,
                    Address = edited_user.Address,
                    Password = edited_user.Password,
                    Type = edited_user.Type,
                    JoiningDate = edited_user.JoiningDate,
                };

                int email_counts = await db_handler.ScalarQueryAsync("SELECT COUNT(*) FROM UserViewModel WHERE EmailId=? AND Id<>?", new object[] { edited_user_view_model.EmailId, edited_user_view_model.Id });
                if (email_counts > 0) { return -2; }

                return await db_handler.UpdateAsync(edited_user_view_model);
            } catch { }
            return -1;
        }

        [HttpGet("CheckUserLogin")]
        public async Task<User> CheckUserLogin(string email_id, string password)
        {
            try
            {
                UserViewModel check_user = await db_handler.FindWithQueryAsync("SELECT * FROM UserViewModel WHERE EmailId=? AND Password=? LIMIT 1", new object[] { email_id, password });
                if (check_user != null)
                {
                    return new User
                    {
                        Id = check_user.Id,
                        Name = check_user.Name,
                        EmailId = check_user.EmailId,
                        Mobile = check_user.Mobile,
                        Address = check_user.Address,
                        Password = check_user.Password,
                        Type = check_user.Type,
                        JoiningDate = check_user.JoiningDate,
                    };
                }
            }
            catch { }
            return new User{ Id = 0, Name = "", EmailId = "", Mobile = "", Address = "", Password = "" };
        }

        [HttpGet("SearchUser")]
        public async Task<User> SearchUser(String user_id)
        {
            try
            {
                UserViewModel search_user = await db_handler.FindWithQueryAsync("SELECT * FROM UserViewModel WHERE Id=? LIMIT 1", new object[] { user_id });
                if (search_user != null)
                {
                    return new User
                    {
                        Id = search_user.Id,
                        Name = search_user.Name,
                        EmailId = search_user.EmailId,
                        Mobile = search_user.Mobile,
                        Address = search_user.Address,
                        Password = search_user.Password,
                        Type = search_user.Type,
                        JoiningDate = search_user.JoiningDate,
                    };
                }
            }
            catch { }
            return new User { Id = 0, Name = "", EmailId = "", Mobile = "", Address = "", Password = "" };
        }
    }
}
