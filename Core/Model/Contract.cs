namespace Core.Model;

public class Contract(Guid id, List<Guid> employeesId, Guid objectToSecureId, Schedule contractTime, Guid? paymentId, Guid clientId, decimal contractSum)
{
    public Guid Id { get; set; } = id;
    public Guid ObjectToSecureId { get; set; } = objectToSecureId;
    public List<Guid> EmployeesId { get; set; } = employeesId;
    public Schedule ContractTime { get; set; } = contractTime;
    public Guid? PaymentId { get; set; } = paymentId;
    public Guid ClientId { get; set; } = clientId;
    public decimal ContractSum { get; set; } = contractSum;

    public bool IsContractPaid()
    {
        return PaymentId != null;
    }
}