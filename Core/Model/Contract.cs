namespace Core.Model;

public class Contract(List<Guid> guardsIds, Guid objectToSecureId, Schedule contractTime, Guid? payment, Guid clientId)
{
    public Guid Id { get; } = Guid.NewGuid();
    public Guid ObjectToSecureId { get; set; } = objectToSecureId;
    public List<Guid> GuardsIds { get; set; } = guardsIds;
    public Schedule ContractTime { get; set; } = contractTime;
    public Guid? PaymentId { get; set; } = payment;
    public Guid ClientId { get; set; } = clientId;
}