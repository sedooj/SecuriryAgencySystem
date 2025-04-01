using Core.Service.Impl;
using SAS.Pages.Clients;
using SAS.Pages.Employees;

namespace SAS.Pages;

public partial class MainMenu : ContentPage
{
    private readonly BaseDataLoader _baseDataLoader = new();

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

    private async void OnReportsButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ReportsPage());
    }

    private void OnLoadBaseDataClicked(object sender, EventArgs e)
    {
        _baseDataLoader.ProcessLoadBaseData();
    }
    
    private void OnDropDataButtonClicked(object sender, EventArgs e)
    {
        _baseDataLoader.DropAll();
    }
}