using Core.Model.Users;
using SAS.Controller;

namespace SAS.Pages.Clients;

public partial class CorporateClientsPage : ContentPage
{
    private readonly CorporateClientController _controller = new();

    public CorporateClientsPage()
    {
        InitializeComponent();
        Bind();
    }

    private void Bind()
    {
        CorporateClientsList.ItemsSource = _controller.CorporateClients;
    }

    private async void OnAddClientClicked(object sender, EventArgs e)
    {
        var addClientPage = new AddCorporateClientPage();
        addClientPage.ClientAdded += (s, newClient) => { _controller.AddClient(newClient); };
        await Navigation.PushAsync(addClientPage);
    }

    private async void OnViewButtonClicked(object sender, EventArgs e)
    {
        if ((sender as ImageButton)?.BindingContext is not CorporateClient selectedClient) return;
        await Navigation.PushAsync(new ViewCorporateClientPage(selectedClient));
    }
}