namespace Core.Model.Users;

public class IndividualClient(Passport passport) : Person(passport)
{
    public Guid ClientId { get; } = Guid.NewGuid();
    public Guid ContractId { get; set; }
}