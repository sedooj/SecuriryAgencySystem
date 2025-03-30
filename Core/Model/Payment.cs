namespace Core.Model;

public class Payment(Guid payerId, DateTime paymentDate, decimal amount)
{
    public Guid Id { get; } = Guid.NewGuid();
    public Guid PayerId { get; set; } = payerId;
    public DateTime PaymentDate { get; set; } = paymentDate;
    public decimal Amount { get; set; } = amount;
}