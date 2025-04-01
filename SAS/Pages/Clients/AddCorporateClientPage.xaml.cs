using Core.Model.Users;

namespace SAS.Pages.Clients;

public partial class AddCorporateClientPage : ContentPage
{
    public event EventHandler<CorporateClient>? ClientAdded;

    public AddCorporateClientPage()
    {
        InitializeComponent();
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var newClient = new CorporateClient(
            Guid.NewGuid(),
            CompanyNameEntry.Text,
            null
        );

        ClientAdded?.Invoke(this, newClient);
        await Navigation.PopAsync();
    }
}