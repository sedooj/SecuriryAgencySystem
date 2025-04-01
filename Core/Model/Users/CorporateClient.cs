namespace Core.Model.Users;

public class CorporateClient
{
    private string _companyName;
    private Guid? _contractId;
    private Guid _id;

    public CorporateClient(Guid id, string companyName, Guid? contractId)
    {
        Id = id;
        CompanyName = companyName;
        ContractId = contractId;
    }

    public Guid Id
    {
        get => _id;
        set
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Id не может быть пустым.");
            _id = value;
        }
    }

    public string CompanyName
    {
        get => _companyName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("CompanyName не может быть пустым или состоять только из пробелов.");
            _companyName = value;
        }
    }

    public Guid? ContractId
    {
        get => _contractId;
        set
        {
            if (value == Guid.Empty)
                throw new ArgumentException("ContractId не может быть пустым Guid.");
            _contractId = value;
        }
    }
}