namespace Core.Model.Users;

public abstract class Person(Passport passport)
{
    public Guid Id { get; } = Guid.NewGuid();
    public Passport Passport { get; set; } = passport;
}