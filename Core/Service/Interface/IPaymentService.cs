using Core.Model;

namespace Core.Service.Interface;

public interface IPaymentService
{
    Contract ProcessPayment(Guid contractId, Guid payerId, decimal amount);
}