using Core.Model;

namespace Core.Service.Interface;

public interface IContractService
{
    void CreateContract(Contract contract);
    void UpdateContract(Contract contract);
    void ArchiveContract(Guid contractId);
    void LinkContractToClient(Guid contractId, Guid clientId, Type clientType);
    void LinkContractToPayment(Guid contractId, Guid paymentId);
}