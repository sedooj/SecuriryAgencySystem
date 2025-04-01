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
        try
        {
            var addClientPage = new AddCorporateClientPage();
            addClientPage.ClientAdded += (s, newClient) => { _controller.AddClient(newClient); };
            await Navigation.PushAsync(addClientPage);
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

    private async void OnViewButtonClicked(object sender, EventArgs e)
    {
        try
        {
            if ((sender as ImageButton)?.BindingContext is not CorporateClient selectedClient) return;
            var viewClientPage = new ViewCorporateClientPage(selectedClient);
            viewClientPage.ContractConcluded += (s, concluded) => { _controller.UpdateTable(); };
            await Navigation.PushAsync(viewClientPage);
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