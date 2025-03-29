namespace SecurityAgencySystem.Model;

public abstract class Person(string name, string surname, string patronymic)
{
    public Guid Id { get; } = Guid.NewGuid();

    public string Name { get; set; } = name;

    public string Surname { get; set; } = surname;

    public string Patronymic { get; set; } = patronymic;
}