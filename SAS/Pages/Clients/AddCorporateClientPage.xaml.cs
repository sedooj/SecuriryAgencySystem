using Core.Model.Users;

namespace SAS.Pages.Clients;

public partial class AddCorporateClientPage : ContentPage
{
    public event EventHandler<CorporateClient>? ClientAdded;

    public AddCorporateClientPage()
    {
        InitializeComponent();
    }

    private void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var newClient = new CorporateClient(
            Guid.NewGuid(),
            CompanyNameEntry.Text,
            Guid.Parse(ContractIdEntry.Text)
        );

        ClientAdded?.Invoke(this, newClient);
        Navigation.PopAsync();
    }
}