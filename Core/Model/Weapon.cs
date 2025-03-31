namespace Core.Model;

public class Weapon(string brand, Guid registrationNumber)
{
    public string Brand { get; } = brand;
    public Guid RegistrationNumber { get; set; } = registrationNumber;
}