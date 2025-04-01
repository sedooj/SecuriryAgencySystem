using Core.Service.Impl;
using Core.Service.Interface;

namespace SAS.Pages
{
    public partial class ReportsPage : ContentPage
    {
        private readonly IReportService _reportService = new ReportService();

        public ReportsPage()
        {
            InitializeComponent();
        }

        private async void OnGenerateDutyScheduleClick(object sender, EventArgs e)
        {
            string report = _reportService.GenerateDutySchedule();
            await DisplayAlert("График дежурств", report, "OK");
        }

        private async void OnGenerateFinancialReportClick(object sender, EventArgs e)
        {
            DateTime startDate = DateTime.Now.AddMonths(-1);
            DateTime endDate = DateTime.Now;
            var report = _reportService.GenerateFinancialReport(startDate, endDate);
            await DisplayAlert("Финансовый отчет", $"Всего получено средств: {report.TotalIncome}₽\nЗа период:{report.Period}.\nКоличество операций: {report.PaymentsCount}.", "OK");
        }

        private async void OnGenerateClientInvoiceClick(object sender, EventArgs e)
        {
            var clients = _reportService.LoadClients();
            var clientNames = clients.Select(c => c.Name).ToArray();

            string selectedClient = await DisplayActionSheet("Выберите клиента", "Отмена", null, clientNames);

            if (selectedClient != "Отмена")
            {
                var client = clients.FirstOrDefault(c => c.Name == selectedClient);
                if (client != null)
                {
                    var report = _reportService.GenerateClientInvoice(client.Id);
                    await DisplayAlert("Счет клиенту", $"Всего вы заплатили: {report.TotalPayed} (Кол-во счетов: {report.InvoicesCount})\nНеоплаченных счетов: {report.NotPaidInvoicesCount}\n", "OK");
                }
            }
        }
    }
}