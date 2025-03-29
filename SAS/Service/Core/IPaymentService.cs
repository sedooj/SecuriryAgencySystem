namespace SAS.Service.Core;

public interface IPaymentService
{
    void ProcessPayment(int contractId, decimal amount);
    bool ValidatePayment(int contractId, decimal amount);
}