using SAS.Pages.Clients;
using SAS.Pages.Employees;

namespace SAS.Pages;

public partial class MainMenu : ContentPage
{
    public MainMenu()
    {
        InitializeComponent();
    }

    private async void OnEmployeesButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EmployeesPage());
    }

    private async void OnClientsButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ClientsPage());
    }

    private void OnDutiesButtonClicked(object sender, EventArgs e)
    {
        // Handle Duties button click
    }

    private void OnReportsButtonClicked(object sender, EventArgs e)
    {
        // Handle Reports button click
    }

    private void OnSettingsButtonClicked(object sender, EventArgs e)
    {
        // Handle Settings button click
    }
}