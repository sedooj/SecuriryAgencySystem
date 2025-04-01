using SAS.Controller;

namespace SAS.Pages.Clients.SecuredObjects;

public partial class SecuredObjectsPage : ContentPage
{
    private readonly SecuredObjectController _controller = new();

    public SecuredObjectsPage()
    {
        InitializeComponent();
        BindingContext = _controller;
        SecuredObjects.ItemsSource = _controller.SecuredObjects;
    }

    private async void OnAddSecuredObjectClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddSecuredObjectPage());
    }
}