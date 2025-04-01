using Core.Impl;
using Core.Interface;
using Core.Model;
using Core.Service.Interface;

namespace Core.Service.Impl;

public class PaymentService : IPaymentService
{
    private readonly IDbService<Contract> _contractDbService = new JsonDbService<Contract>();
    private readonly IDbService<Payment> _paymentDbService = new JsonDbService<Payment>();

    public Contract ProcessPayment(Guid contractId, Guid payerId, decimal amount)
    {
        if (!ValidatePayment(amount)) throw new ArgumentException("Payment amount is invalid");
        var payment = new Payment(Guid.NewGuid(), payerId, DateTime.Now, amount);
        _paymentDbService.SaveEntity(payment);
        var contract = _contractDbService.LoadEntity(contractId) ??
                       throw new ArgumentNullException($"Contract with id {contractId} not found");
        contract.PaymentId = payment.Id;
        _contractDbService.UpdateEntity(contractId, contract);
        return contract;
    }

    private bool ValidatePayment(decimal amount)
    {
        return amount > 0;
    }
}