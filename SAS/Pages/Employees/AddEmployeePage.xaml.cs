using Core.Model;
using Core.Model.Users;

namespace SAS.Pages.Employees
{
    public partial class AddEmployeePage : ContentPage
    {
        public event EventHandler<Employee>? EmployeeAdded;

        public AddEmployeePage()
        {
            InitializeComponent();
            PopulateRolePicker();
        }

        private void PopulateRolePicker()
        {
            RolePicker.ItemsSource = Enum.GetValues(typeof(Role)).Cast<Role>().ToList();
        }

        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (RolePicker.SelectedItem is not Role selectedRole)
            {
                DisplayAlert("Ошибка", "Пожалуйста, выберите роль.", "OK");
                return;
            }

            Employee newEmployee = new Employee(
                new Passport(PassportSeriesEntry.Text, PassportNumberEntry.Text, PassportIssueDatePicker.Date,
                    FirstNameEntry.Text, LastNameEntry.Text, MiddleNameEntry.Text, GenderEntry.Text, CountryEntry.Text),
                new JobRole(PositionEntry.Text, selectedRole),
                new Documents(AddressEntry.Text, InnEntry.Text)
            );

            EmployeeAdded?.Invoke(this, newEmployee);
            Navigation.PopAsync();
        }
    }
}