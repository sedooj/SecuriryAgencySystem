namespace Core.Model.Users;

public class IndividualClient(Guid id, Passport passport, Guid? contractId) : Person(passport, id)
{
    public Guid? ContractId { get; set; } = contractId;
}