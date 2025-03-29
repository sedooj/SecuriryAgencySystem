namespace Core.Model.Users;

public class CorporateClient(string companyName, Guid contractId)
{
    public Guid Id { get; } = Guid.NewGuid();
    public string CompanyName { get; set; } = companyName;
    public Guid ContractId { get; set; } = contractId;
}