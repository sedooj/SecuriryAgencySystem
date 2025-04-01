using System.Text;
using Core.Impl;
using Core.Interface;
using Core.Model;
using Core.Model.Users;
using Core.Service.Interface;

namespace Core.Service.Impl;

public class ReportService : IReportService
{
    private readonly IDbService<Contract> _contractDb = new JsonDbService<Contract>();
    private readonly IDbService<CorporateClient> _corporateClientDb = new JsonDbService<CorporateClient>();
    private readonly IDutyScheduleService _dutyScheduleService = new DutyScheduleService();
    private readonly IDbService<IndividualClient> _individualClientDb = new JsonDbService<IndividualClient>();
    private readonly IDbService<Payment> _paymentDb = new JsonDbService<Payment>();

    public string GenerateDutySchedule()
    {
        var dutySchedules = _dutyScheduleService.LoadAllDutySchedules();

        var report = new StringBuilder();
        foreach (var employee in dutySchedules)
        {
            var employeeDuties = dutySchedules.Where(d => d.Employee.Id == employee.Employee.Id).ToList();
            if (employeeDuties.Any())
            {
                report.AppendLine($"Сотрудник: {employee.Employee.Passport.FullName}");
                foreach (var duty in employeeDuties)
                    report.AppendLine($"  Дежурство: {duty.Duty.Schedule.WorkingDate}");
            }
        }

        return report.ToString();
    }

    public FinancialReport GenerateFinancialReport(DateTime startDate, DateTime endDate)
    {
        var payments = _paymentDb.LoadEntities().Sum(p => p.Amount);
        var paymentsCount = _paymentDb.LoadEntities().Count;
        return new FinancialReport
        {
            TotalIncome = payments,
            Period = $"с {startDate.ToShortDateString()} по {endDate.ToShortDateString()}",
            PaymentsCount = paymentsCount
        };
    }

    public ClientInvoice GenerateClientInvoice(Guid clientId)
    {
        var payments = _paymentDb.LoadEntities();
        var clientPayments = payments.Where(p => p.PayerId == clientId).ToList();
        var clientContracts = _contractDb.LoadEntities().Where(c => !c.IsContractPaid()).ToList();

        return new ClientInvoice
        {
            TotalPayed = clientPayments.Sum(p => p.Amount),
            InvoicesCount = clientPayments.Count,
            NotPaidInvoicesCount = clientContracts.Count
        };
    }

    public List<MergedClient> LoadClients()
    {
        var individualClients = _individualClientDb.LoadEntities();
        var corporateClients = _corporateClientDb.LoadEntities();
        var clients = new List<MergedClient>();

        clients.AddRange(individualClients.Select(c => new MergedClient { Id = c.Id, Name = c.Passport.FullName, ClientType = OwnerType.Individual}));
        clients.AddRange(corporateClients.Select(c => new MergedClient { Id = c.Id, Name = c.CompanyName, ClientType = OwnerType.Corp}));

        return clients;
    }

    public class MergedClient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public OwnerType ClientType { get; set; }
    }

    public class ClientInvoice
    {
        public decimal TotalPayed { get; set; }
        public int NotPaidInvoicesCount { get; set; }
        public int InvoicesCount { get; set; }
    }

    public class FinancialReport
    {
        public decimal TotalIncome { get; set; }
        public string Period { get; set; }
        public int PaymentsCount { get; set; }
    }
}