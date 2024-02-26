using System.Text.RegularExpressions;

namespace ShopEase;

public partial class ProductsPage : ContentPage
{
    api_handler api_service = new api_handler();

    IEnumerable<Product> products;
    int selected_max_quantity;

    Product selected_product = null;

    public ProductsPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        clear_fields();
        load_data_Clicked(load_data, new EventArgs());
    }

    private void reset_Clicked(object sender, EventArgs e)
    {
        clear_fields();
    }

    private void clear_fields()
    {
        products_list.SelectedItem = null;
        retailer.Text = string.Empty;
        address.Text = string.Empty;
        price.Text = string.Empty;
        quantity.Text = string.Empty;
        total.Text = string.Empty;
        selected_max_quantity = 0;
        selected_product = null;
    }

    private async void products_list_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        selected_product = (Product)e.Item;
        selected_max_quantity = selected_product.Stock;

        address.Text = AppShell.active_user.Address;
        price.Text = selected_product.Price.ToString();
        quantity.Text = "1";

        User search_user = await api_service.SearchUser(selected_product.RetailerId.ToString());
        if (search_user != null && search_user.Id != 0)
        {
            retailer.Text = search_user.Name;
        }
        else { retailer.Text = ""; }
    }

    private async void load_data_Clicked(object sender, EventArgs e)
    {
        var response = await api_service.GetAvailableProducts();
        if (response != null && response is List<Product>)
        {
            products = (List<Product>)response;
            products_list.ItemsSource = products;
            if (products.Count() == 0) { await Shell.Current.DisplayAlert("Message", "No products found to display!\nNew products will be available soon once retailers add them.", "Ok"); }
        }
        else { await DisplayAlert("Alert", "Oops, something went wrong! Try again later!", "Ok"); }
    }

    private async void order_Clicked(object sender, EventArgs e)
    {
        string order_retailer = AppShell.filter_input(retailer.Text);
        string order_address = AppShell.filter_input(address.Text);
        string order_quantity = AppShell.filter_input(quantity.Text);
        string order_total = AppShell.filter_input(total.Text);
        string order_price = AppShell.filter_input(price.Text);

        if (order_retailer == "" || order_price == "" || order_address == "" || order_quantity == "" || order_total == "") { await DisplayAlert("Alert", "All fields are required for placing order!", "Ok"); }
        else if (selected_product != null)
        {
            try
            {
                int? result = await api_service.AddOrder(new Order
                {
                    ConsumerId = AppShell.active_user.Id,
                    ProductId = selected_product.Id,
                    ProductName = selected_product.Name,
                    ProductDescription = selected_product.Description,
                    Quantity = int.Parse(order_quantity),
                    Price = int.Parse(order_price),
                    TotalAmount = int.Parse(order_total),
                    Address = order_address
                });
                if (result == null) { await DisplayAlert("Alert", "Oops, something went wrong! Try again later!", "Ok"); }
                else if (result == -2) { await DisplayAlert("Alert", "Product with entered name already exists!", "Ok"); }
                else if (result > 0)
                {
                    clear_fields();
                    load_data_Clicked(load_data, new EventArgs());
                    await Shell.Current.DisplayAlert("Success", "Congratulations, payment confirmed and your order has been placed, keep checking orders history page for tracking order.", "Ok");
                }
                else { await DisplayAlert("Alert", "Oops, something went wrong! Try again later!", "Ok"); }
            }
            catch { await DisplayAlert("Alert", "Invalid inputs! Try again later!", "Ok"); }
        }
        else { await DisplayAlert("Alert", "Select a product first to place order", "Ok"); }
    }

    private void quantity_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            if (quantity.Text == "") { return; }
            quantity.Text = String.Join("", Regex.Matches(quantity.Text, @"\d+"));
            int qty = int.Parse(quantity.Text);
            if (qty < 1) { quantity.Text = "1"; }
            else if (qty > selected_max_quantity && selected_max_quantity != 0) { quantity.Text = selected_max_quantity.ToString(); }
            total.Text = (qty * int.Parse(price.Text)).ToString();
        }
        catch { quantity.Text = ""; }
    }
}