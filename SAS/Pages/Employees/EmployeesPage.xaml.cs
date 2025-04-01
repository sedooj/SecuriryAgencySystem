using Core.Model.Users;
using SAS.Controller;

namespace SAS.Pages.Employees;

public partial class EmployeesPage : ContentPage, IPage
{
    private readonly EmployeeController _controller = new();

    public EmployeesPage()
    {
        InitializeComponent();
        Init();
        Bind();
    }

    public void Init()
    {
        BindingContext = this;
    }

    public void Bind()
    {
        EmployeesList.ItemsSource = _controller.Employees;
    }

    private async void OnAddEmployeeClicked(object sender, EventArgs e)
    {
        try
        {
            var addEmployeePage = new AddEmployeePage();
            addEmployeePage.EmployeeAdded += (s, newEmployee) => { _controller.AddEmployee(newEmployee); };
            await Navigation.PushAsync(addEmployeePage);
        }
        catch (InvalidOperationException ex)
        {
            await DisplayAlert("Ошибка", $"Ошибка выполнения операции: {ex.Message}", "OK");
        }
        catch (ArgumentNullException ex)
        {
            await DisplayAlert("Ошибка", $"Отсутствует необходимый аргумент: {ex.Message}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }
    
    private async void OnViewButtonClicked(object sender, EventArgs e)
    {
        try
        {
            if ((sender as ImageButton)?.BindingContext is not Employee selectedEmployee) return;
            await Navigation.PushModalAsync(new ViewEmployeePage(selectedEmployee));
        }
        catch (InvalidOperationException ex)
        {
            await DisplayAlert("Ошибка", $"Ошибка выполнения операции: {ex.Message}", "OK");
        }
        catch (ArgumentNullException ex)
        {
            await DisplayAlert("Ошибка", $"Отсутствует необходимый аргумент: {ex.Message}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }
}