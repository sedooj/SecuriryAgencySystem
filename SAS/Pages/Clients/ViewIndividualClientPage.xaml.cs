using Core.Model.Users;
using Core.Service.Impl;
using Core.Service.Interface;

namespace SAS.Pages.Clients;

public partial class ViewIndividualClientPage : ContentPage
{
    private readonly IContractService _contractService = new ContractService();

    public ViewIndividualClientPage(IndividualClient client)
    {
        InitializeComponent();
        BindClientData(client);
    }

    public event EventHandler<IndividualClient>? ContractConcluded;

    private void BindClientData(IndividualClient client)
    {
        FullNameLabel.Text = "ФИО: " + client.Passport.FullName;
        PassportNumberLabel.Text = "Номер паспорта: " + client.Passport.PassportNumber;
        PassportSeriesLabel.Text = "Серия паспорта: " + client.Passport.PassportSeries;
        PassportIssueDateLabel.Text = "Дата выдачи паспорта: " + client.Passport.IssueDate.ToShortDateString();
        GenderLabel.Text = "Пол: " + client.Passport.Gender;
        CountryLabel.Text = "Страна: " + client.Passport.Nationality;
        ContractIdLabel.Text = "Контракт: " + client.ContractId;
        CreateContractButton.BindingContext = client;
    }

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnCreateContractButtonClicked(object sender, EventArgs e)
    {
        try
        {
            if (sender is Button { BindingContext: IndividualClient individualClient })
            {
                var createContractPage = new CreateContractPage(individualClient);
                createContractPage.ContractConcluded += (s, contract) =>
                {
                    try
                    {
                        _contractService.ProcessCreateContract(contract, individualClient.GetType());
                        ContractConcluded?.Invoke(this, individualClient);
                        ContractIdLabel.Text = "Контракт: " + contract.Id;
                    }
                    catch (InvalidOperationException ex)
                    {
                        DisplayAlert("Ошибка", $"Ошибка выполнения операции: {ex.Message}", "OK");
                    }
                    catch (ArgumentNullException ex)
                    {
                        DisplayAlert("Ошибка", $"Отсутствует необходимый аргумент: {ex.Message}", "OK");
                    }
                };
                await Navigation.PushAsync(createContractPage);
            }
        }
        catch (InvalidOperationException ex)
        {
            await DisplayAlert("Ошибка", $"Ошибка выполнения операции: {ex.Message}", "OK");
        }
        catch (ArgumentNullException ex)
        {
            await DisplayAlert("Ошибка", $"Отсутствует необходимый аргумент: {ex.Message}", "OK");
        }
    }
}