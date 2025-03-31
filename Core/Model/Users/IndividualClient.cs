namespace Core.Model.Users;

public class IndividualClient(Guid id, Passport passport, Guid clientId) : Person(passport, id)
{
    public Guid ClientId { get; set; } = clientId;
    public Guid ContractId { get; set; }
}