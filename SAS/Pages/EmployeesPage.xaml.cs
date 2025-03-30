using Core.Model;
using SAS.Controller;
using Core.Model.Users;

namespace SAS.Pages
{
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

        private void OnAddEmployeeClicked(object sender, EventArgs e)
        {
            var newEmployee = new Employee(new Passport("125", "225", DateTime.Now, "Сергендер", "Сергей", "Сергеевич", "Male", "Россия"),
                                           new JobRole("Менеджер", Role.Manager), new Documents("Улица Чехова", "230"));
            _controller.AddEmployee(newEmployee);
        }
        
        private void OnEditEmployeeClicked(object sender, EventArgs e)
        {
            if (EmployeesList.SelectedItem is not Employee selectedEmployee) return;
            selectedEmployee.JobRole.Position = "Старший менеджер";
            _controller.UpdateEmployee(selectedEmployee);
        }

        private void OnDismissEmployeeClicked(object sender, EventArgs e)
        {
            if (EmployeesList.SelectedItem is not Employee selectedEmployee) return;
            _controller.RemoveEmployee(selectedEmployee);
        }

        private void OnDeleteEmployeeClicked(object sender, EventArgs e)
        {
            if (EmployeesList.SelectedItem is not Employee selectedEmployee) return;
            _controller.RemoveEmployee(selectedEmployee);
        }

        private void OnViewButtonClicked(object sender, EventArgs e)
        {
            if ((sender as ImageButton)?.BindingContext is not Employee selectedEmployee) return;
            DisplayAlert("Просмотр сотрудника", $"ФИО: {selectedEmployee.Passport.Name}\nДолжность: {selectedEmployee.JobRole.Position}", "OK");
        }
    }
}