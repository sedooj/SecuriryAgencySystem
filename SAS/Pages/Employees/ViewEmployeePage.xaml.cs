using Core.Model.Users;

namespace SAS.Pages.Employees
{
    public partial class ViewEmployeePage : ContentPage
    {
        public ViewEmployeePage(Employee employee)
        {
            InitializeComponent();
            BindEmployeeData(employee);
        }

        private void BindEmployeeData(Employee employee)
        {
            FirstNameLabel.Text = "Имя: " + employee.Passport.Name;
            LastNameLabel.Text = "Фамилия: " + employee.Passport.Surname;
            MiddleNameLabel.Text = "Отчество: " + employee.Passport.Patronymic;
            PositionLabel.Text = "Должность: " + employee.JobRole.Position;
            AddressLabel.Text = "Адрес проживания: " + employee.Documents.Address;
            PassportNumberLabel.Text = "Номер и серия паспорта: " + employee.Passport.PassportNumber + " " + employee.Passport.PassportSeries;
            PassportIssueDateLabel.Text = "Дата выдачи паспорта: " + employee.Passport.IssueDate.ToShortDateString();
            GenderLabel.Text = "Пол: " + employee.Passport.Gender;
            CountryLabel.Text = "Страна: " + employee.Passport.Nationality;
            InnLabel.Text = "ИНН: " + employee.Documents.Inn;
            PfrIdLabel.Text = "Номер ПФР: " + employee.Documents.PfrId;
        }

        private void OnCloseButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}