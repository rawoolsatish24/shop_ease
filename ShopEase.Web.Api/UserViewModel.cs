using SQLite;

namespace ShopEase.Web.Api
{
    public class UserViewModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? EmailId { get; set; }

        public string? Mobile { get; set; }

        public string? Address { get; set; }

        public string? Password { get; set; }

        public string Type { get; set; } = "C";

        public DateTime JoiningDate { get; set; } = DateTime.Now;
    }
}
