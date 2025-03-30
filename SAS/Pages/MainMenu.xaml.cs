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

    private void OnClientsButtonClicked(object sender, EventArgs e)
    {
        // Handle Clients button click
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