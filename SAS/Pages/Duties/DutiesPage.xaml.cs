using SAS.Controller;

namespace SAS.Pages.Duties;

public partial class DutiesPage : ContentView
{
    private readonly DutiesController _dutiesController = new();

    public DutiesPage()
    {
        InitializeComponent();
        LoadDuties();
    }

    private void LoadDuties()
    {
        _dutiesController.UpdateTable();
        DutiesCollectionView.ItemsSource = _dutiesController.EmployeeDutySchedules;
    }

    private void OnRefreshClicked(object sender, EventArgs e)
    {
        LoadDuties();
    }
}