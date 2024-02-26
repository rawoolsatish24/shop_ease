namespace ShopEase;

public partial class SignupPage : ContentPage
{
    api_handler api_service = new api_handler();

    public SignupPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        reset_Clicked(reset, new EventArgs());
    }

    private void reset_Clicked(object sender, EventArgs e)
    {
        first_name.Text = string.Empty;
        last_name.Text = string.Empty;
        email.Text = string.Empty;
        mobile.Text = string.Empty;
        address.Text = string.Empty;
        password.Text = string.Empty;
        confirm_password.Text = string.Empty;
    }

    private void goto_login(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("///LoginPage");
    }

    private async void signup_Clicked(object sender, EventArgs e)
    {
        error.Text = "Please wait, thank you!";

        string signup_first_name = AppShell.filter_input(first_name.Text);
        string signup_last_name = AppShell.filter_input(last_name.Text);
        string signup_email = AppShell.filter_input(email.Text);
        string signup_mobile = AppShell.filter_input(mobile.Text);
        string signup_address = AppShell.filter_input(address.Text);
        string signup_password = AppShell.filter_input(password.Text);
        string signup_confirm_password = AppShell.filter_input(confirm_password.Text);
        string signup_user_type = (consumer.IsChecked) ? consumer.Value.ToString() : retailer.Value.ToString();

        if (signup_first_name == "" || signup_last_name == "" || signup_email == "" || signup_mobile == "" || signup_address == "" || signup_password == "" || signup_confirm_password == "")
        {
            error.Text = "All fields are required for signup!";
        }
        else if(signup_password != signup_confirm_password)
        {
            error.Text = "Signup password and confirm password didn't matched!";
        }
        else
        {
            int? result = await api_service.AddUser(new User
            {
                Name = (signup_first_name + " " + signup_last_name).Trim(),
                EmailId = signup_email,
                Mobile = signup_mobile,
                Address = signup_address,
                Password = signup_password,
                Type = signup_user_type
            });
            if (result == null) { error.Text = "Oops, something went wrong! Try again later!"; }
            else if (result == -2) { error.Text = "Account with entered email-id already exists!"; }
            else if (result > 0)
            {
                reset_Clicked(reset, new EventArgs());
                error.Text = "Signup successfull!";
                await DisplayAlert("Congratulations", "New account created successfully!\nWelcome to ShopEase.", "Ok");
                await Shell.Current.GoToAsync("///LoginPage");
            }
            else { error.Text = "Oops, something went wrong! Try again later!"; }
        }
    }

    private void name_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender == first_name) { first_name.Text = first_name.Text.Replace(" ", ""); }
        else if (sender == last_name) { last_name.Text = last_name.Text.Replace(" ", ""); }
        else if (sender == email) { email.Text = email.Text.Replace(" ", ""); }
        else if (sender == mobile) { mobile.Text = mobile.Text.Replace(" ", ""); }
        else if (sender == password) { password.Text = password.Text.Replace(" ", ""); }
        else if (sender == confirm_password) { confirm_password.Text = confirm_password.Text.Replace(" ", ""); }
    }
}