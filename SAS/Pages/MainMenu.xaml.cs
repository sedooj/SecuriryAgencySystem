using SAS.Controller;
using SAS.Pages.Clients;
using SAS.Pages.Duties;
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

    private async void OnDutiesButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DutiesPage());
    }

    private void OnReportsButtonClicked(object sender, EventArgs e)
    {
        // Handle Reports button click
    }

    private void OnSettingsButtonClicked(object sender, EventArgs e)
    {
        // Handle Settings button click
    }
    
    private void OnCreateSecuredObjectButtonClicked(object sender, EventArgs e)
    {
        SecuredObjectController securedObjectController = new();
        securedObjectController.Setup();
    }
}