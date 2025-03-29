namespace Core.Model;

public class Contract(Guid clientId, DateTime startDate, DateTime endDate, decimal amount)
{
    public Guid Id { get; } = Guid.NewGuid();
    public Guid ClientId { get; init; } = clientId;
    public DateTime StartDate { get; init; } = startDate;
    public DateTime EndDate { get; set; } = endDate;
    public decimal Amount { get; set; } = amount;
}