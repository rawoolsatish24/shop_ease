namespace ShopEase
{
    public partial class AppShell : Shell
    {
        public static User active_user = null;

        public AppShell()
        {
            InitializeComponent();
        }

        public void switch_splash_screen()
        {
            manage_tabs();
            welcome_splash_screen.IsVisible = false;
            Shell.Current.GoToAsync("///LoginPage");
        }

        public void manage_tabs()
        {
            bool user_is_active = (active_user != null);
            bool user_is_consumer = (user_is_active && active_user.Type == "C");

            about_page.IsVisible = true;
            profile_page.IsVisible = user_is_active;
            manage_products_page.IsVisible = (user_is_active && !user_is_consumer);
            manage_orders_page.IsVisible = (user_is_active && !user_is_consumer);
            products_page.IsVisible = (user_is_active && user_is_consumer);
            orders_history_page.IsVisible = (user_is_active && user_is_consumer);

            login_page.IsVisible = !user_is_active;
            signup_page.IsVisible = !user_is_active;

            consumer_dashboard_page.IsVisible = (user_is_active && user_is_consumer);
            retailer_dashboard_page.IsVisible = (user_is_active && !user_is_consumer);
        }

        public static string filter_input(string text)
        {
            text = text.Trim();
            text = text.ToUpper();
            text = text.Replace("|", "");
            text = text.Replace("\"", "");
            text = text.Replace("\'", "");
            return text;
        }
    }
}