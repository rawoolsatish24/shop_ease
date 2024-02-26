namespace ShopEase
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            initialize_splash_screen();
        }

        private async void initialize_splash_screen()
        {
            await Task.Delay(2000);
            ((AppShell)App.Current.MainPage).switch_splash_screen();
        }
    }
}