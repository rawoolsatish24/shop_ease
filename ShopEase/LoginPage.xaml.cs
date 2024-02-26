namespace ShopEase;

public partial class LoginPage : ContentPage
{
    api_handler api_service = new api_handler();

    public LoginPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        reset_Clicked(reset, new EventArgs());
    }

    private void goto_sign_up(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("///SignupPage");
    }

    private void reset_Clicked(object sender, EventArgs e)
    {
        email.Text = string.Empty;
        password.Text = string.Empty;
        error.Text = string.Empty;
    }

    private async void login_Clicked(object sender, EventArgs e)
    {
        error.Text = "Please wait, thank you!";

        string login_email = AppShell.filter_input(email.Text);
        string login_password = AppShell.filter_input(password.Text);

        if (login_email == "" || login_password == "")
        {
            error.Text = "Both email and password are required for login!";
        }
        else
        {
            User login_user = await api_service.CheckUserLogin(login_email, login_password);
            if (login_user == null) { error.Text = "Oops, something went wrong! Try again later!"; }
            else if (login_user.Id == 0) { error.Text = "Sorry, login details not found! Signup if new user!"; }
            else
            {
                reset_Clicked(reset, new EventArgs());
                AppShell.active_user = login_user;
                error.Text = "Login successfull!";
                await DisplayAlert("Welcome", "Login successfull", "Ok");
                ((AppShell)App.Current.MainPage).manage_tabs();
                if (login_user.Type == "C") { await Shell.Current.GoToAsync("///ConsumerDashboardPage"); }
                else { await Shell.Current.GoToAsync("///RetailerDashboardPage"); }
            }
        }
    }
}