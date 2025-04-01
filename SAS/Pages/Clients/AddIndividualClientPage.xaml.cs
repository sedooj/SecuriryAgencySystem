using Core.Model;
using Core.Model.Users;

namespace SAS.Pages.Clients;

public partial class AddIndividualClientPage : ContentPage
{
    public event EventHandler<IndividualClient>? ClientAdded;

    public AddIndividualClientPage()
    {
        InitializeComponent();
    }

    private void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var newClient = new IndividualClient(
            Guid.NewGuid(),
            new Passport(
                PassportSeriesEntry.Text,
                PassportNumberEntry.Text,
                PassportIssueDatePicker.Date,
                FirstNameEntry.Text,
                LastNameEntry.Text,
                MiddleNameEntry.Text,
                GenderEntry.Text,
                CountryEntry.Text
            ),
            null
        );

        ClientAdded?.Invoke(this, newClient);
        Navigation.PopAsync();
    }
}