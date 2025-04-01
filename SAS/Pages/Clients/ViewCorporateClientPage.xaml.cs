using Core.Model.Users;
using Core.Service.Impl;
using Core.Service.Interface;

namespace SAS.Pages.Clients;

public partial class ViewCorporateClientPage : ContentPage
{
    private readonly IContractService _contractService = new ContractService();
    public event EventHandler<CorporateClient>? ContractConcluded; 

    public ViewCorporateClientPage(CorporateClient client)
    {
        InitializeComponent();
        BindClientData(client);
    }

    private void BindClientData(CorporateClient client)
    {
        CompanyNameLabel.Text = "Название компании: " + client.CompanyName;
        ContractIdLabel.Text = "ID Контракта: " + client.ContractId;
        CreateContractButton.BindingContext = client;
    }

    private void OnCloseButtonClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
    
    private async void OnCreateContractButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button { BindingContext: CorporateClient corporateClient})
        {
            var createContractPage = new CreateContractPage(corporateClient);
            createContractPage.ContractConcluded += (s, contract) =>
            {
                _contractService.ProcessCreateContract(contract, corporateClient.GetType());
                ContractConcluded?.Invoke(this, corporateClient);
                ContractIdLabel.Text = "Контракт: " + contract.Id;
            };
            await Navigation.PushAsync(createContractPage);
        }
    }
}