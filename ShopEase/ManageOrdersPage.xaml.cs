namespace ShopEase;

public partial class ManageOrdersPage : ContentPage
{
    api_handler api_service = new api_handler();
    IEnumerable<Order> orders;

    public ManageOrdersPage()
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
        string new_status = "";
        Order selected_order = (Order)e.Item;
        if (selected_order != null)
        {
            if (selected_order.Status == "ORDER PLACED")
            {
                string response = await Shell.Current.DisplayPromptAsync("Need input", "Update order status:\nEnter [P]ROCESSING or [R]EJECTED\n", maxLength: 1);
                if (response != null)
                {
                    new_status = (response.ToUpper() == "P") ? "PROCESSING" : (response.ToUpper() == "R") ? "REJECTED" : "";
                    if (new_status == "") { return; }

                    selected_order.Status = new_status;
                    new_status = "";
                    int? result = await api_service.UpdateOrder(selected_order);
                    if (result != null && result > 0)
                    {
                        load_data_Clicked(load_data, new EventArgs());
                        await DisplayAlert("Alert", "Order updated successfully!", "Ok");
                    }
                    else { await DisplayAlert("Alert", "Oops, something went wrong! Try again later!", "Ok"); }
                }
                return;
            }
            else if (selected_order.Status == "PROCESSING") { new_status = "OUT FOR DEL."; }
            else if (selected_order.Status == "OUT FOR DEL.") { new_status = "DELIVERED"; }
            if(new_status != "")
            {
                selected_order.Status = new_status;
                new_status = "";
                int? result = await api_service.UpdateOrder(selected_order);
                if (result != null && result > 0)
                {
                    load_data_Clicked(load_data, new EventArgs());
                    await DisplayAlert("Alert", "Order updated successfully!", "Ok");
                }
                else { await DisplayAlert("Alert", "Oops, something went wrong! Try again later!", "Ok"); }
            }
        }
    }

    private async void load_data_Clicked(object sender, EventArgs e)
    {
        var response = await api_service.GetOrdersForRetailer(AppShell.active_user.Id.ToString());
        if (response != null && response is List<Order>)
        {
            orders = (List<Order>)response;
            orders_list.ItemsSource = orders;
            if (orders.Count() == 0) { await Shell.Current.DisplayAlert("Message", "No orders found to display!\nPlace new order from Products page.", "Ok"); }
        }
        else { await DisplayAlert("Alert", "Oops, something went wrong! Try again later!", "Ok"); }
    }
}