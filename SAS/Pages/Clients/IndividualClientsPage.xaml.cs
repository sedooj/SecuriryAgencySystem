using Core.Model.Users;
using SAS.Controller;

namespace SAS.Pages.Clients;

public partial class IndividualClientsPage : ContentPage
{
    private readonly IndividualClientController _controller = new();

    public IndividualClientsPage()
    {
        InitializeComponent();
        Bind();
    }

    private void Bind()
    {
        IndividualClientsList.ItemsSource = _controller.IndividualClients;
    }

    private async void OnAddClientClicked(object sender, EventArgs e)
    {
        var addClientPage = new AddIndividualClientPage();
        addClientPage.ClientAdded += (s, newClient) => { _controller.AddClient(newClient); };
        await Navigation.PushAsync(addClientPage);
    }

    private async void OnViewButtonClicked(object sender, EventArgs e)
    {
        if ((sender as ImageButton)?.BindingContext is not IndividualClient selectedClient) return;
        var viewClientPage = new ViewIndividualClientPage(selectedClient);
        viewClientPage.ContractConcluded += (s, concluded) => { _controller.UpdateTable(); };
        await Navigation.PushAsync(viewClientPage);
    }
}