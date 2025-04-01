namespace Core.Model;

public class Weapon(Guid id, string brand)
{
    public Guid Id { get; set; } = id;
    public string Brand { get; } = brand;
}