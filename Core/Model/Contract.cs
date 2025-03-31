namespace Core.Model;

public class Contract(List<Guid> employeesId, Guid objectToSecureId, Schedule contractTime, Guid? payment, Guid clientId)
{
    public Guid Id { get; } = Guid.NewGuid();
    public Guid ObjectToSecureId { get; set; } = objectToSecureId;
    public List<Guid> EmployeesId { get; set; } = employeesId;
    public Schedule ContractTime { get; set; } = contractTime;
    public Guid? PaymentId { get; set; } = payment;
    public Guid ClientId { get; set; } = clientId;
}