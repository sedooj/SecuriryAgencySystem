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
        try
        {
            var addClientPage = new AddIndividualClientPage();
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
            if ((sender as ImageButton)?.BindingContext is not IndividualClient selectedClient) return;
            var viewClientPage = new ViewIndividualClientPage(selectedClient);
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