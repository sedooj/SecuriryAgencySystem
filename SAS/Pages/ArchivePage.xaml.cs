using Core.Service.Impl;

namespace SAS.Pages;

public partial class ArchivePage : ContentPage
{
    private readonly EmployeeService _employeeService = new();

    public ArchivePage()
    {
        InitializeComponent();
        LoadFiredEmployees();
    }

    private void LoadFiredEmployees()
    {
        var firedEmployees = _employeeService.LoadFiredEmployees();
        FiredEmployeesList.ItemsSource = firedEmployees;
    }
}