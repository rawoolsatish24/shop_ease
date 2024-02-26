namespace ShopEase;

public partial class ConsumerDashboardPage : ContentPage
{
    api_handler api_service = new api_handler();

    public ConsumerDashboardPage()
	{
		InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        full_name.Text = "Welcome, " + AppShell.active_user.Name;
        user_joining_date.Text = " " + AppShell.active_user.JoiningDate.ToString("dd MMM, yyyy");

        int? result;

        result = await api_service.GetConsumerUniqueItemsCount(AppShell.active_user.Id.ToString());
        if (result != null && result >= 0) { unique_items.Text = result.ToString(); }
        else { unique_items.Text = "-NULL-"; }

        result = await api_service.GetConsumerTotalOrdersCount(AppShell.active_user.Id.ToString());
        if (result != null && result >= 0) { total_orders.Text = result.ToString(); }
        else { total_orders.Text = "-NULL-"; }

        result = await api_service.GetConsumerTotalPurchaseAmount(AppShell.active_user.Id.ToString());
        if (result != null && result >= 0) { total_purchases.Text = "$" + result.ToString(); }
        else { total_purchases.Text = "-NULL-"; }
    }

    private async void logout_Clicked(object sender, EventArgs e)
    {
        AppShell.active_user = null;
        await DisplayAlert("Message", "Thanks for visiting, See you soon!", "Ok");
        ((AppShell)App.Current.MainPage).manage_tabs();
        await Shell.Current.GoToAsync("///LoginPage");
    }
}