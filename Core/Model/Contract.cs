namespace Core.Model;

public class Contract
{
    private Guid _clientId;
    private decimal _contractSum;
    private Schedule _contractTime;
    private Guid _id;
    private Guid _objectToSecureId;

    public Contract(
        Guid id,
        List<Guid> employeesId,
        Guid objectToSecureId,
        Schedule contractTime,
        Guid? paymentId,
        Guid clientId,
        decimal contractSum)
    {
        Id = id;
        EmployeesId = employeesId;
        ObjectToSecureId = objectToSecureId;
        ContractTime = contractTime;
        PaymentId = paymentId;
        ClientId = clientId;
        ContractSum = contractSum;
    }

    public Guid Id
    {
        get => _id;
        set
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Id не может быть пустым Guid.");
            _id = value;
        }
    }

    public Guid ObjectToSecureId
    {
        get => _objectToSecureId;
        set
        {
            if (value == Guid.Empty)
                throw new ArgumentException("ObjectToSecureId не может быть пустым Guid.");
            _objectToSecureId = value;
        }
    }

    public List<Guid> EmployeesId { get; set; }

    public Schedule ContractTime
    {
        get => _contractTime;
        set
        {
            if (value == null)
                throw new ArgumentException("ContractTime не может быть null.");
            _contractTime = value;
        }
    }

    public Guid? PaymentId { get; set; }

    public Guid ClientId
    {
        get => _clientId;
        set
        {
            if (value == Guid.Empty)
                throw new ArgumentException("ClientId не может быть пустым Guid.");
            _clientId = value;
        }
    }

    public decimal ContractSum
    {
        get => _contractSum;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Сумма оплаты договора должна быть больше 0.");
            _contractSum = value;
        }
    }

    public bool IsContractPaid()
    {
        return PaymentId != null;
    }
}