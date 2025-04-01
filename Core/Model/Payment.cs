namespace Core.Model;

public class Payment
{
    private decimal _amount;
    private Guid _id;
    private Guid _payerId;
    private DateTime _paymentDate;

    public Payment(Guid id, Guid payerId, DateTime paymentDate, decimal amount)
    {
        Id = id;
        PayerId = payerId;
        PaymentDate = paymentDate;
        Amount = amount;
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

    public Guid PayerId
    {
        get => _payerId;
        set
        {
            if (value == Guid.Empty)
                throw new ArgumentException("PayerId не может быть пустым Guid.");
            _payerId = value;
        }
    }

    public DateTime PaymentDate
    {
        get => _paymentDate;
        set
        {
            if (value == default)
                throw new ArgumentException("Дата оплаты не может быть пустой.");
            _paymentDate = value;
        }
    }

    public decimal Amount
    {
        get => _amount;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Сумма оплаты должна быть больше 0.");
            _amount = value;
        }
    }
}