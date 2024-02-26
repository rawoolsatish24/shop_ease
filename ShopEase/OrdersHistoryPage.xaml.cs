namespace ShopEase;

public partial class OrdersHistoryPage : ContentPage
{
    api_handler api_service = new api_handler();
    IEnumerable<Order> orders;

    public OrdersHistoryPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        load_data_Clicked(load_data, new EventArgs());
    }

    private async void orders_list_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Order selected_order = (Order)e.Item;
        if(selected_order != null)
        {
            if(selected_order.Status == "ORDER PLACED" || selected_order.Status == "PROCESSING")
            {
                if (await Shell.Current.DisplayAlert("Alert", "Do you want to cancel this selected order?", "Yes", "No"))
                {
                    selected_order.Status = "CANCELED";
                    int? result = await api_service.UpdateOrder(selected_order);
                    if (result != null && result > 0)
                    {
                        load_data_Clicked(load_data, new EventArgs());
                        await DisplayAlert("Alert", "Order edited successfully!", "Ok");
                    }
                    else { await DisplayAlert("Alert", "Oops, something went wrong! Try again later!", "Ok"); }
                }
            }
        }
    }

    private async void load_data_Clicked(object sender, EventArgs e)
    {
        var response = await api_service.GetConsumerOrders(AppShell.active_user.Id.ToString());
        if (response != null && response is List<Order>)
        {
            orders = (List<Order>)response;
            orders_list.ItemsSource = orders;
            if (orders.Count() == 0) { await Shell.Current.DisplayAlert("Message", "No orders found to display!\nPlace new order from Products page.", "Ok"); }
        }
        else { await DisplayAlert("Alert", "Oops, something went wrong! Try again later!", "Ok"); }
    }
}