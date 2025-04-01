using Core.Model;

namespace Core.Service.Interface;

public interface IContractService
{
    Contract CreateContract(Contract contract);
    Contract UpdateContract(Contract contract);
    Contract ArchiveContract(Guid contractId);
    void LinkContractToClient(Guid contractId, Guid clientId, Type clientType);
    Contract PayContract(Guid contractId, Guid payerId, decimal amount);
    decimal CalculateContractAmount(SecuredObject securedObject);
    void ProcessCreateContract(Contract contract, Type clientType);
    Contract AssignStaffToContract(Contract contract, List<Guid> securities);
}