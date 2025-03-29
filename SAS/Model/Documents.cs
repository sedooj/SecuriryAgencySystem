namespace SecurityAgencySystem.Model;

public class Documents(string address, string inn)
{
    public string Address { get; set; } = address;

    public string Inn { get; set; } = inn;

    public Guid PfrId { get; set; } = Guid.NewGuid();
}