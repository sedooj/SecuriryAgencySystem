using Core.Model.Users;

namespace SAS.Pages.Clients;

public partial class ViewCorporateClientPage : ContentPage
{
    public ViewCorporateClientPage(CorporateClient client)
    {
        InitializeComponent();
        BindClientData(client);
    }

    private void BindClientData(CorporateClient client)
    {
        CompanyNameLabel.Text = "Название компании: " + client.CompanyName;
        ContractIdLabel.Text = "ID Контракта: " + client.ContractId;
    }

    private void OnCloseButtonClicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}