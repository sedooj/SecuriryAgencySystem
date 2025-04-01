using Core.Model;
using Core.Service.Impl;

namespace Core.Service.Interface;

public interface IReportService
{
    string GenerateDutySchedule();
    ReportService.FinancialReport GenerateFinancialReport(DateTime startDate, DateTime endDate);
    ReportService.ClientInvoice GenerateClientInvoice(Guid clientId);
    public List<ReportService.MergedClient> LoadClients();
}