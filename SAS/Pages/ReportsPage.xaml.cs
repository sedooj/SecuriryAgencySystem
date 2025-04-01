using Core.Service.Impl;
using Core.Service.Interface;

namespace SAS.Pages;

public partial class ReportsPage : ContentPage
{
    private readonly IReportService _reportService = new ReportService();

    public ReportsPage()
    {
        InitializeComponent();
    }

    private async void OnGenerateDutyScheduleClick(object sender, EventArgs e)
    {
        try
        {
            var report = _reportService.GenerateDutySchedule();
            await DisplayAlert("График дежурств", report, "OK");
        }
        catch (InvalidOperationException ex)
        {
            await DisplayAlert("Ошибка", $"Ошибка выполнения операции: {ex.Message}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }

    private async void OnGenerateFinancialReportClick(object sender, EventArgs e)
    {
        try
        {
            DateTime startDate = DateTime.Now.AddMonths(-1);
            DateTime endDate = DateTime.Now;
            var report = _reportService.GenerateFinancialReport(startDate, endDate);
            await DisplayAlert("Финансовый отчет",
                $"Всего получено средств: {report.TotalIncome}₽\nЗа период: {report.Period}.\nКоличество операций: {report.PaymentsCount}.",
                "OK");
        }
        catch (InvalidOperationException ex)
        {
            await DisplayAlert("Ошибка", $"Ошибка выполнения операции: {ex.Message}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }

    private async void OnGenerateClientInvoiceClick(object sender, EventArgs e)
    {
        try
        {
            var clients = _reportService.LoadClients();
            var clientNames = clients.Select(c => c.Name).ToArray();

            var selectedClient = await DisplayActionSheet("Выберите клиента", "Отмена", null, clientNames);

            if (selectedClient != "Отмена")
            {
                var client = clients.FirstOrDefault(c => c.Name == selectedClient);
                if (client != null)
                {
                    var report = _reportService.GenerateClientInvoice(client.Id);
                    await DisplayAlert("Счет клиенту",
                        $"Всего вы заплатили: {report.TotalPayed} (Кол-во счетов: {report.InvoicesCount})\nНеоплаченных счетов: {report.NotPaidInvoicesCount}\n",
                        "OK");
                }
            }
        }
        catch (InvalidOperationException ex)
        {
            await DisplayAlert("Ошибка", $"Ошибка выполнения операции: {ex.Message}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }
}