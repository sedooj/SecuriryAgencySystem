namespace Core.Model.Users;

public class CorporateClient(Guid id, string companyName, Guid? contractId)
{
    public Guid Id { get; set; } = id;
    public string CompanyName { get; set; } = companyName;
    public Guid? ContractId { get; set; } = contractId;
}