using Core.Model;
using Core.Model.Users;
using SAS.Controller;

namespace SAS.Pages.Duties
{
    public partial class AddDutyPage : ContentPage
    {
        public event EventHandler<EmployeeDutySchedule>? DutyAdded;
        private readonly DutiesController _dutiesController;
        private readonly SecuredObjectController _securedObjectController = new ();

        public AddDutyPage(DutiesController dutiesController)
        {
            InitializeComponent();
            _dutiesController = dutiesController;
            LoadEmployees();
            LoadSecuredObjects();
        }

        private void LoadEmployees()
        {
            EmployeePicker.ItemsSource = _dutiesController.GetAllEmployees();
            EmployeePicker.ItemDisplayBinding = new Binding("Passport.FullName");
        }

        private void LoadSecuredObjects()
        {
            SecuredObjectPicker.ItemsSource = _securedObjectController.GetObjects();
            SecuredObjectPicker.ItemDisplayBinding = new Binding("Name");
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (EmployeePicker.SelectedItem is Employee selectedEmployee && SecuredObjectPicker.SelectedItem is SecuredObject selectedObject)
            {
                var newDuty = new DutySchedule(
                    "Regular",
                    null,
                    new Schedule(DateEntry.Date, DateEntry.Date.AddHours(TimeEntry.Time.Hours))
                );
                
                var employeeDutySchedule = new EmployeeDutySchedule(selectedEmployee, newDuty, selectedObject.Id);
                DutyAdded?.Invoke(this, employeeDutySchedule);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Ошибка", "Пожалуйста, выберите ох��анника и объект", "OK");
            }
        }

        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}