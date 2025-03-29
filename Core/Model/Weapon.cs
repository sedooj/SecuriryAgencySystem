namespace Core.Model;

public class Weapon(string brand)
{
    public string Brand { get; } = brand;
    public Guid RegistrationNumber { get; } = Guid.NewGuid();
}