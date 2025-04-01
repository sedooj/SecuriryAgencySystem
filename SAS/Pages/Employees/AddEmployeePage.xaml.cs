using Core.Model;
using Core.Model.Users;

namespace SAS.Pages.Employees;

public partial class AddEmployeePage : ContentPage
{
    public AddEmployeePage()
    {
        InitializeComponent();
        PopulateRolePicker();
    }

    public event EventHandler<Employee>? EmployeeAdded;

    private void PopulateRolePicker()
    {
        RolePicker.ItemsSource = Enum.GetValues(typeof(Role)).Cast<Role>().ToList();
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        try
        {
            if (RolePicker.SelectedItem is not Role selectedRole)
            {
                await DisplayAlert("Ошибка", "Пожалуйста, выберите роль.", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(PassportSeriesEntry.Text) ||
                string.IsNullOrWhiteSpace(PassportNumberEntry.Text) ||
                string.IsNullOrWhiteSpace(FirstNameEntry.Text) ||
                string.IsNullOrWhiteSpace(LastNameEntry.Text) ||
                string.IsNullOrWhiteSpace(MiddleNameEntry.Text) ||
                string.IsNullOrWhiteSpace(GenderEntry.Text) ||
                string.IsNullOrWhiteSpace(CountryEntry.Text) ||
                string.IsNullOrWhiteSpace(PositionEntry.Text) ||
                string.IsNullOrWhiteSpace(AddressEntry.Text) ||
                string.IsNullOrWhiteSpace(InnEntry.Text))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, заполните все поля.", "OK");
                return;
            }
            var newEmployee = new Employee(
                new Passport(PassportSeriesEntry.Text, PassportNumberEntry.Text, PassportIssueDatePicker.Date,
                    FirstNameEntry.Text, LastNameEntry.Text, MiddleNameEntry.Text, GenderEntry.Text, CountryEntry.Text),
                Guid.NewGuid(), Guid.NewGuid(), new JobRole(PositionEntry.Text, selectedRole),
                new Documents(AddressEntry.Text, InnEntry.Text), null, null, null, null, null);

            EmployeeAdded?.Invoke(this, newEmployee);
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", ex.Message, "OK");
        }
    }
}