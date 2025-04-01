namespace Core.Model;

public class Payment(Guid id, Guid payerId, DateTime paymentDate, decimal amount)
{
    public Guid Id { get; set; } = id;
    public Guid PayerId { get; set; } = payerId;
    public DateTime PaymentDate { get; set; } = paymentDate;
    public decimal Amount { get; set; } = amount;
}