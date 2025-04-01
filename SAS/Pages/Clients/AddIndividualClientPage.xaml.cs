using Core.Model;
using Core.Model.Users;

namespace SAS.Pages.Clients;

public partial class AddIndividualClientPage : ContentPage
{
    public AddIndividualClientPage()
    {
        InitializeComponent();
    }

    public event EventHandler<IndividualClient>? ClientAdded;

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        try
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
            await Navigation.PopAsync();
        }
        catch (InvalidOperationException ex)
        {
            await DisplayAlert("Ошибка", $"Ошибка выполнения операции: {ex.Message}", "OK");
        }
        catch (ArgumentNullException ex)
        {
            await DisplayAlert("Ошибка", $"Отсутствует необходимый аргумент: {ex.Message}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }
}