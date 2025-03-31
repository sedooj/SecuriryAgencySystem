using Core.Model.Users;
using SAS.Controller;

namespace SAS.Pages.Employees
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

        private async void OnAddEmployeeClicked(object sender, EventArgs e)
        {
            var addEmployeePage = new AddEmployeePage();
            addEmployeePage.EmployeeAdded += (s, newEmployee) =>
            {
                _controller.AddEmployee(newEmployee);
            };
            await Navigation.PushAsync(addEmployeePage);
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

        private async void OnViewButtonClicked(object sender, EventArgs e)
        {
            if ((sender as ImageButton)?.BindingContext is not Employee selectedEmployee) return;
            await Navigation.PushModalAsync(new ViewEmployeePage(selectedEmployee));
        }
    }
}