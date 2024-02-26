using System.Text.RegularExpressions;

namespace ShopEase;

public partial class ManageProductsPage : ContentPage
{
    api_handler api_service = new api_handler();

    int active_product_id = 0;
    DateTime active_product_creation_date = DateTime.Now;

    IEnumerable<Product> products;

    public ManageProductsPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        clear_fields();
        load_data_Clicked(load_data, new EventArgs());
    }

    private async void load_data_Clicked(object sender, EventArgs e)
    {
        clear_fields();
        var response = await api_service.GetRetailerProducts(AppShell.active_user.Id.ToString());
        if (response != null && response is List<Product>)
        {
            products = (List<Product>)response;
            products_list.ItemsSource = products;
            if (products.Count() == 0) { await Shell.Current.DisplayAlert("Message", "No products found to display! Add new products above.", "Ok"); }
        }
        else { await DisplayAlert("Alert", "Oops, something went wrong! Try again later!", "Ok"); }
    }

    private void products_list_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Product selected_product = (Product)e.Item;
        
        active_product_id = selected_product.Id;
        active_product_creation_date = selected_product.CreationDate;

        product_name.Text = selected_product.Name;
        product_description.Text = selected_product.Description;
        product_price.Text = selected_product.Price.ToString();
        product_stock.Text = selected_product.Stock.ToString();
    }

    private async void save_Clicked(object sender, EventArgs e)
    {
        string name = AppShell.filter_input(product_name.Text);
        string price = AppShell.filter_input(product_price.Text);
        string stock = AppShell.filter_input(product_stock.Text);
        string description = AppShell.filter_input(product_description.Text);

        if (name == "" || price == "" || stock == "" || description == "") { await DisplayAlert("Alert", "All fields are required for signup!", "Ok"); }
        else
        {
            try
            {
                int int_price = int.Parse(price);
                int int_stock = int.Parse(stock);

                if(active_product_id == 0)
                {
                    manage_product(new Product
                    {
                        RetailerId = AppShell.active_user.Id,
                        RetailerName = AppShell.active_user.Name,
                        Name = name,
                        Description = description,
                        Price = int_price,
                        Stock = int_stock
                    });
                }
                else
                {
                    manage_product(new Product
                    {
                        Id = active_product_id,
                        RetailerId = AppShell.active_user.Id,
                        RetailerName = AppShell.active_user.Name,
                        Name = name,
                        Description = description,
                        Price = int_price,
                        Stock = int_stock,
                        CreationDate = active_product_creation_date
                    });
                }
            } catch { await DisplayAlert("Alert", "Invalid inputs for product price and stock details!", "Ok"); }
        }
    }

    private async void manage_product(Product new_edit_product)
    {
        int? result = (active_product_id == 0) ? await api_service.AddProduct(new_edit_product) : await api_service.UpdateProduct(new_edit_product);
        if (result == null) { await DisplayAlert("Alert", "Oops, something went wrong! Try again later!", "Ok"); }
        else if (result == -2) { await DisplayAlert("Alert", "Product with entered name already exists!", "Ok"); }
        else if (result > 0)
        {
            clear_fields();
            load_data_Clicked(load_data, new EventArgs());
            if (active_product_id == 0) { await DisplayAlert("Congratulations", "New product added successfully!", "Ok"); }
            else { await DisplayAlert("Congratulations", "Product edited successfully!", "Ok"); }
        }
        else { await DisplayAlert("Alert", "Oops, something went wrong! Try again later!", "Ok"); }
    }

    private void reset_Clicked(object sender, EventArgs e)
    {
        clear_fields();
    }

    private void clear_fields()
    {
        product_name.Text = string.Empty;
        product_price.Text = string.Empty;
        product_stock.Text = string.Empty;
        product_description.Text = string.Empty;
        active_product_id = 0;
    }

    private void product_price_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            if (product_price.Text == "") { return; }
            product_price.Text = String.Join("", Regex.Matches(product_price.Text, @"\d+"));
            int price = int.Parse(product_price.Text);
            if (price < 1) { product_price.Text = "1"; }
        }
        catch { product_price.Text = ""; }
    }

    private void product_stock_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            if (product_stock.Text == "") { return; }
            product_stock.Text = String.Join("", Regex.Matches(product_stock.Text, @"\d+"));
            int price = int.Parse(product_stock.Text);
            if (price < 0) { product_stock.Text = "0"; }
        }
        catch { product_stock.Text = ""; }
    }
}