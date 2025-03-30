namespace Core.Service.Interface;

public interface IPaymentService
{
    void ProcessPayment(Guid contractId, Guid payerId, decimal amount);
}