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

    public Contract CreateContract(Contract contract)
    {
        foreach (var employeeId in contract.EmployeesId)
        {
            var guardian = _employeeDbService.LoadEntity(employeeId) ??
                           throw new NullReferenceException($"No guardian found with id {employeeId}");
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
                         throw new ArgumentException($"Individual client with id {clientId} not found");
            client.ContractId = contractId;
            _individualClientDbService.UpdateEntity(clientId, client);
        }
        else if (clientType == typeof(CorporateClient))
        {
            var client = _corporateClientDbService.LoadEntity(clientId) ??
                         throw new ArgumentException($"Corporate client with id {clientId} not found");
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
                                $"Secured object with id {contract.ObjectToSecureId} not found");
        foreach (var security in securities)
        {
            var employee = _employeeDbService.LoadEntity(security) ??
                           throw new NullReferenceException($"No employee found with id {security}");
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

    private List<Guid> FindGuardianForContract(Contract contract)
    {
        var availableGuardians = _employeeDbService.LoadEntities()
            .Where(e => e.JobRole.Role == Role.SecurityOfficer && e.SecuringObjectId == null)
            .ToList();

        var securedObject = _securedObjectDbService.LoadEntities()
                                .FirstOrDefault(o => o.Id == contract.ObjectToSecureId) ??
                            throw new NullReferenceException("Secured object not found");

        if (availableGuardians.Count < securedObject.GuardiansCount)
            throw new InvalidOperationException("Not enough available guardians for the contract.");

        return availableGuardians.Take(securedObject.GuardiansCount).Select(e => e.Id).ToList();
    }
}