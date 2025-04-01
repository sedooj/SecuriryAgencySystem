namespace Core.Model.Users;

public abstract class Person(Passport passport, Guid id)
{
    public Guid Id { get; set; } = id;
    public Passport Passport { get; set; } = passport;
}