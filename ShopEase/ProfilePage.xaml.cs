namespace ShopEase;

public partial class ProfilePage : ContentPage
{
    api_handler api_service = new api_handler();

    public ProfilePage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        load_user_details();
    }

    private void load_user_details()
    {
        string[] name = AppShell.active_user.Name.Split(' ');
        first_name.Text = name[0];
        last_name.Text = name[1];
        email.Text = AppShell.active_user.EmailId;
        mobile.Text = AppShell.active_user.Mobile;
        address.Text = AppShell.active_user.Address;

        old_password.Text = "";
        new_password.Text = "";
        confirm_password.Text = "";
    }

    private void name_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender == first_name) { first_name.Text = first_name.Text.Replace(" ", ""); }
        else if (sender == last_name) { last_name.Text = last_name.Text.Replace(" ", ""); }
        else if (sender == email) { email.Text = email.Text.Replace(" ", ""); }
        else if (sender == mobile) { mobile.Text = mobile.Text.Replace(" ", ""); }
        else if (sender == old_password) { old_password.Text = old_password.Text.Replace(" ", ""); }
        else if (sender == new_password) { new_password.Text = new_password.Text.Replace(" ", ""); }
        else if (sender == confirm_password) { confirm_password.Text = confirm_password.Text.Replace(" ", ""); }
    }

    private void update_profile_Clicked(object sender, EventArgs e)
    {
        error.Text = "Please wait, thank you!";

        string profile_first_name = AppShell.filter_input(first_name.Text);
        string profile_last_name = AppShell.filter_input(last_name.Text);
        string profile_email = AppShell.filter_input(email.Text);
        string profile_mobile = AppShell.filter_input(mobile.Text);
        string profile_address = AppShell.filter_input(address.Text);

        if (profile_first_name == "" || profile_last_name == "" || profile_email == "" || profile_mobile == "" || profile_address == "")
        {
            error.Text = "All fields are required for signup!";
        }
        else
        {
            update_profile_details(new User
            {
                Id = AppShell.active_user.Id,
                Name = (profile_first_name + " " + profile_last_name).Trim(),
                EmailId = profile_email,
                Mobile = profile_mobile,
                Address = profile_address,
                Password = AppShell.active_user.Password,
                Type = AppShell.active_user.Type,
                JoiningDate = AppShell.active_user.JoiningDate
            });
        }
    }

    private void update_password_Clicked(object sender, EventArgs e)
    {
        error.Text = "Please wait, thank you!";

        string profile_old_password = AppShell.filter_input(old_password.Text);
        string profile_new_password = AppShell.filter_input(new_password.Text);
        string profile_confirm_password = AppShell.filter_input(confirm_password.Text);

        if (profile_old_password == "" || profile_new_password == "" || profile_confirm_password == "") { error.Text = "All fields are required for signup!"; }
        else if (AppShell.active_user.Password != profile_old_password) { error.Text = "Incorrect old password!"; }
        else if (profile_new_password == profile_old_password) { error.Text = "New password cannot be same as old password!"; }
        else if (profile_new_password != profile_confirm_password) { error.Text = "New password and confirm password must be same!"; }
        else
        {
            update_profile_details(new User
            {
                Id = AppShell.active_user.Id,
                Name = AppShell.active_user.Name,
                EmailId = AppShell.active_user.EmailId,
                Mobile = AppShell.active_user.Mobile,
                Address = AppShell.active_user.Address,
                Password = profile_new_password,
                Type = AppShell.active_user.Type,
                JoiningDate = AppShell.active_user.JoiningDate
            });
        }
    }

    private async void update_profile_details(User edited_user)
    {
        int? result = await api_service.UpdateUser(edited_user);
        if (result == null) { error.Text = "Oops, something went wrong! Try again later!"; }
        else if (result == -2) { error.Text = "Account with entered email-id already exists!"; }
        else if (result > 0)
        {
            User search_user = await api_service.SearchUser(edited_user.Id.ToString());
            if(search_user == null || search_user.Id == 0)
            {
                error.Text = "Oops, something went wrong!";
                await DisplayAlert("Error", "Oops, something went wrong! Please login again.", "Ok");
            }
            else
            {
                AppShell.active_user = search_user;
                load_user_details();
                error.Text = "Profile updated successfully!";
            }
        }
        else { error.Text = "Oops, something went wrong! Try again later!"; }
    }
}