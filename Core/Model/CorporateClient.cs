namespace Core.Model;

public class CorporateClient(string name, string surname, string patronymic) : Person(name, surname, patronymic)
{
    public Guid ClientId { get; } = Guid.NewGuid();
}