using Core.Model.Users;

namespace SAS.Pages.Clients;

public partial class ViewIndividualClientPage : ContentPage
{
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
    }

    private void OnCloseButtonClicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}