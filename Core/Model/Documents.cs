namespace Core.Model;

public class Documents
{
    private string _address;
    private string _inn;

    public Documents(string address, string inn)
    {
        Address = address;
        Inn = inn;
    }

    public string Address
    {
        get => _address;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 1 || value.Length > 200)
                throw new ArgumentException("Адресс должен быть длиной от 1 до 200 символов.");
            _address = value;
        }
    }

    public string Inn
    {
        get => _inn;
        set
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d{10}$"))
                throw new ArgumentException("ИНН Должен быть 10-значным числом.");
            _inn = value;
        }
    }

    public Guid PfrId { get; set; } = Guid.NewGuid();
}