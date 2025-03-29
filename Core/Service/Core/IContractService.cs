using Core.Model;

namespace Core.Service.Core;

public interface IContractService
{
    void CreateContract(Contract contract);
    void UpdateContract(Contract contract);
    void ArchiveContract(int contractId);
    void LinkContractToClient(int contractId, int clientId);
    void LinkContractToPayment(int contractId, int paymentId);
}