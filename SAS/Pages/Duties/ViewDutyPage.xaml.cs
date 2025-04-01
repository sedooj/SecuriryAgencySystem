using Core.Model;

namespace SAS.Pages.Duties
{
    public partial class ViewDutyPage : ContentPage
    {
        public ViewDutyPage(EmployeeDutySchedule dutySchedule)
        {
            InitializeComponent();
            DisplayDutyDetails(dutySchedule);
        }

        private void DisplayDutyDetails(EmployeeDutySchedule dutySchedule)
        {
            DateLabel.Text = $"Дата: {dutySchedule.Duty.Schedule.StartDate.ToShortDateString()}";
            TimeLabel.Text = $"Время: {dutySchedule.Duty.Schedule.StartDate.ToShortTimeString()} - {dutySchedule.Duty.Schedule.EndDate.ToShortTimeString()}";
            GuardLabel.Text = $"Охранник: {dutySchedule.Employee.Passport.FullName}";
            ObjectLabel.Text = $"Объект: {dutySchedule.Duty.ScheduleType}";
        }

        private void OnCloseButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}