namespace SAS.Pages.Clients;

public partial class ClientsPage : ContentPage
{
    public ClientsPage()
    {
        InitializeComponent();
    }

    private async void OnIndividualClientsButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new IndividualClientsPage());
    }

    private async void OnCorporateClientsButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CorporateClientsPage());
    }
}