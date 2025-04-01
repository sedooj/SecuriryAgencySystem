using Core.Impl;
using Core.Interface;
using Core.Model;
using Core.Model.Users;
using Core.Service.Interface;

namespace Core.Service.Impl;

public class ContractService : IContractService
{
    private readonly IDbService<Contract> _contractDbService = new JsonDbService<Contract>();
    private readonly IDbService<IndividualClient> _individualClientDbService = new JsonDbService<IndividualClient>();
    private readonly IDbService<CorporateClient> _corporateClientDbService = new JsonDbService<CorporateClient>();
    private readonly IDbService<Employee> _employeeDbService = new JsonDbService<Employee>();
    private readonly IPaymentService _paymentService = new PaymentService();
    
    public void CreateContract(Contract contract)
    {
        foreach (var employeeId in contract.EmployeesId)
        {
            var guardian = _employeeDbService.LoadEntity(employeeId) ?? throw new NullReferenceException($"No guardian found with id {employeeId}");
            guardian.SecuringObjectId = contract.ObjectToSecureId;
            _employeeDbService.UpdateEntity(employeeId, guardian);
        }
        _contractDbService.SaveEntity(contract);
    }

    public void UpdateContract(Contract contract)
    {
        _contractDbService.UpdateEntity(contract.Id, contract);
    }

    public void ArchiveContract(Guid contractId)
    {
        throw new NotImplementedException();
    }

    public void LinkContractToClient(Guid contractId, Guid clientId, Type clientType)
    {
        if (clientType == typeof(IndividualClient))
        {
            var client = _individualClientDbService.LoadEntity(clientId) ?? throw new ArgumentException($"Individual client with id {clientId} not found");
            client.ContractId = contractId;
            _individualClientDbService.UpdateEntity(clientId, client);
        }
        else if (clientType == typeof(CorporateClient))
        {
            var client = _corporateClientDbService.LoadEntity(clientId) ?? throw new ArgumentException($"Corporate client with id {clientId} not found");
            client.ContractId = contractId;
            _corporateClientDbService.UpdateEntity(clientId, client);
        }
    }

    public void PayContract(Guid contractId, Guid payerId, decimal amount)
    {
        _paymentService.ProcessPayment(contractId, payerId, amount);
    }

    public decimal CalculateContractAmount(SecuredObject securedObject)
    {
        decimal baseRate = 800m;
        decimal securityLevelMultiplier = securedObject.SecurityLevel switch
        {
            SecurityLevel.Low => 1.0m,
            SecurityLevel.Medium => 1.5m,
            SecurityLevel.High => 2.0m,
            SecurityLevel.Hard => 2.5m,
            _ => 1.0m
        };
        decimal guardiansCost = securedObject.GuardiansCount * 9500m;
        decimal contractAmount = ((decimal) securedObject.Area * baseRate * securityLevelMultiplier) + guardiansCost;
        return contractAmount;
    }

    public void ProcessCreateContract(Contract contract, Type clientType)
    {
        CreateContract(contract);
        LinkContractToClient(contract.Id, contract.ClientId, clientType);
        PayContract(contract.Id, contract.ClientId, contract.ContractSum);
    }
}