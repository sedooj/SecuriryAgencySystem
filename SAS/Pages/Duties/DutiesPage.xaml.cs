using Core.Model;
using Core.Model.Users;
using SAS.Controller;

namespace SAS.Pages.Duties
{
    public partial class DutiesPage : ContentPage
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

        private async void OnAddDutyButtonClicked(object sender, EventArgs e)
        {
            var addDutyPage = new AddDutyPage(_dutiesController);
            addDutyPage.DutyAdded += (s, duty) =>
            {
                _dutiesController.AddDuty(duty.Employee.Id, duty.Duty, duty.SecuringObjectId);
            };
            await Navigation.PushAsync(addDutyPage);
        }

        private async void OnViewButtonClicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            var duty = button?.BindingContext as EmployeeDutySchedule;
            if (duty != null)
            {
                await Navigation.PushAsync(new ViewDutyPage(duty));
            }
        }
    }
}