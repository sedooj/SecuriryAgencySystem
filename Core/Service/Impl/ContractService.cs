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
    private readonly IDbService<Guardian> _guardianDbService = new JsonDbService<Guardian>();

    private readonly IPaymentService _paymentService = new PaymentService();
    
    public void CreateContract(Contract contract)
    {
        foreach (var guardId in contract.GuardsIds)
        {
            var guardian = _guardianDbService.LoadEntity(guardId) ?? throw new NullReferenceException($"No guardian found with id {guardId}");
            guardian.SecuringObjectId = contract.ObjectToSecureId;
            _guardianDbService.UpdateEntity(guardId, guardian);
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
}