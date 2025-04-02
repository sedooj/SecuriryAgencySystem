using Core.Impl;
using Core.Interface;
using Core.Model;
using Core.Model.Users;
using Core.Service.Interface;

namespace Core.Service.Impl;

public class ContractService : IContractService
{
    private readonly IDbService<Contract> _contractDbService = new JsonDbService<Contract>();
    private readonly IDbService<CorporateClient> _corporateClientDbService = new JsonDbService<CorporateClient>();
    private readonly IDbService<Employee> _employeeDbService = new JsonDbService<Employee>();
    private readonly IDbService<IndividualClient> _individualClientDbService = new JsonDbService<IndividualClient>();
    private readonly IPaymentService _paymentService = new PaymentService();
    private readonly IDbService<SecuredObject> _securedObjectDbService = new JsonDbService<SecuredObject>();
    private readonly IEmployeeService _employeeService = new EmployeeService();
    public Contract CreateContract(Contract contract)
    {
        foreach (var employeeId in contract.EmployeesId)
        {
            var guardian = _employeeDbService.LoadEntity(employeeId) ??
                           throw new NullReferenceException($"Охранник с id {employeeId} не найден");
            guardian.SecuringObjectId = contract.ObjectToSecureId;
            _employeeDbService.UpdateEntity(employeeId, guardian);
        }

        _contractDbService.SaveEntity(contract);
        return contract;
    }

    public Contract UpdateContract(Contract contract)
    {
        _contractDbService.UpdateEntity(contract.Id, contract);
        return contract;
    }

    public Contract ArchiveContract(Guid contractId)
    {
        throw new NotImplementedException();
    }

    public void LinkContractToClient(Guid contractId, Guid clientId, Type clientType)
    {
        if (clientType == typeof(IndividualClient))
        {
            var client = _individualClientDbService.LoadEntity(clientId) ??
                         throw new ArgumentException($"Частный клиент с id {clientId} не найден");
            client.ContractId = contractId;
            _individualClientDbService.UpdateEntity(clientId, client);
        }
        else if (clientType == typeof(CorporateClient))
        {
            var client = _corporateClientDbService.LoadEntity(clientId) ??
                         throw new ArgumentException($"Корпоративный клиент с id {clientId} не найден");
            client.ContractId = contractId;
            _corporateClientDbService.UpdateEntity(clientId, client);
        }
    }

    public Contract PayContract(Guid contractId, Guid payerId, decimal amount)
    {
        return _paymentService.ProcessPayment(contractId, payerId, amount);
    }

    public decimal CalculateContractAmount(SecuredObject securedObject)
    {
        var baseRate = 800m;
        var securityLevelMultiplier = securedObject.SecurityLevel switch
        {
            SecurityLevel.Low => 1.0m,
            SecurityLevel.Medium => 1.5m,
            SecurityLevel.High => 2.0m,
            SecurityLevel.Hard => 2.5m,
            _ => 1.0m
        };
        var guardiansCost = securedObject.GuardiansCount * 9500m;
        var contractAmount = (decimal)securedObject.Area * baseRate * securityLevelMultiplier + guardiansCost;
        return contractAmount;
    }

    public void ProcessCreateContract(Contract contract, Type clientType)
    {
        contract = CreateContract(contract);
        LinkContractToClient(contract.Id, contract.ClientId, clientType);
        contract = PayContract(contract.Id, contract.ClientId, contract.ContractSum);
        contract = AssignStaffToContract(contract, FindGuardianForContract(contract));
    }

    public Contract AssignStaffToContract(Contract contract, List<Guid> securities)
    {
        var securedObject = _securedObjectDbService.LoadEntity(contract.ObjectToSecureId) ??
                            throw new NullReferenceException(
                                $"Объект для охраны с id {contract.ObjectToSecureId} не найден.");
        foreach (var security in securities)
        {
            var employee = _employeeDbService.LoadEntity(security) ??
                           throw new NullReferenceException($"Работник с id {security} не найден");
            employee.SecuringObjectId = securedObject.Id;
            employee.SecuringObjectName = securedObject.Name;
            employee.Schedule = new DutySchedule(
                "Полная занятость",
                null,
                new Schedule(
                    contract.ContractTime.StartDate,
                    contract.ContractTime.EndDate
                ));
            _employeeDbService.UpdateEntity(security, employee);
        }

        contract.EmployeesId.AddRange(securities);
        _contractDbService.UpdateEntity(contract.Id, contract);
        return contract;
    }

    public void FindReplacementForFiredEmployee(Guid employeeId)
    {
        var contracts = _contractDbService.LoadEntities()
            .Where(c => c.EmployeesId.Contains(employeeId))
            .ToList();

        if (!contracts.Any())
        {
            _employeeService.FireEmployee(employeeId);
            return;
        }

        var contract = contracts.First();

        var firedEmployee = _employeeDbService.LoadEntity(employeeId) ??
                            throw new ArgumentException($"Работник с id {employeeId} не найден");

        var availableGuardians = _employeeDbService.LoadEntities()
            .Where(e => e.JobRole.Role == Role.SecurityOfficer && e.SecuringObjectId == null)
            .ToList();

        if (!availableGuardians.Any())
            throw new InvalidOperationException("Нет доступных охранников для замены.");

        var newSecurityForContract = availableGuardians.First();
        newSecurityForContract.SecuringObjectId = firedEmployee.SecuringObjectId;
        newSecurityForContract.SecuringObjectName = firedEmployee.SecuringObjectName;
        newSecurityForContract.Schedule = firedEmployee.Schedule;
        if (newSecurityForContract.Schedule != null)
            newSecurityForContract.Schedule.Replacement = new Replacement(firedEmployee.Id,
                "Замена уволенного сотрудника");

        _employeeDbService.UpdateEntity(newSecurityForContract.Id, newSecurityForContract);

        contract.EmployeesId.Remove(employeeId);
        contract.EmployeesId.Add(newSecurityForContract.Id);
        _contractDbService.UpdateEntity(contract.Id, contract);
    }

    private List<Guid> FindGuardianForContract(Contract contract)
    {
        var availableGuardians = _employeeDbService.LoadEntities()
            .Where(e => e.JobRole.Role == Role.SecurityOfficer && e.SecuringObjectId == null)
            .ToList();

        var securedObject = _securedObjectDbService.LoadEntities()
                                .FirstOrDefault(o => o.Id == contract.ObjectToSecureId) ??
                            throw new NullReferenceException("Объект для охраны не найден.");

        if (availableGuardians.Count < securedObject.GuardiansCount)
            throw new InvalidOperationException("Недостаточно охранников для выполнения контракта.");

        return availableGuardians.Take(securedObject.GuardiansCount).Select(e => e.Id).ToList();
    }
}