using System.ComponentModel;
using Core.Model.Users;
using Core.Service.Impl;
using Core.Service.Interface;
using SAS.Controller;

namespace SAS.Pages.Clients;

public partial class ViewIndividualClientPage : ContentPage
{
    private readonly IContractService _contractService = new ContractService();
    public event EventHandler<IndividualClient>? ContractConcluded;

    public ViewIndividualClientPage(IndividualClient client)
    {
        InitializeComponent();
        BindClientData(client);
    }

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

    private void OnCloseButtonClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private async void OnCreateContractButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button { BindingContext: IndividualClient individualClient })
        {
            var createContractPage = new CreateContractPage(individualClient);
            createContractPage.ContractConcluded += (s, contract) =>
            {
                _contractService.ProcessCreateContract(contract, individualClient.GetType());
                ContractConcluded?.Invoke(this, individualClient);
                ContractIdLabel.Text = "Контракт: " + contract.Id;
            };
            await Navigation.PushAsync(createContractPage);
        }
        
    }
}