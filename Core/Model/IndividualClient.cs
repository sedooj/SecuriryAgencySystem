namespace Core.Model;

public class IndividualClient(string name, string surname, string patronymic) : Person(name, surname, patronymic)
{
    public Guid ClientId { get; } = Guid.NewGuid();
}