namespace Core.Model;

public class Weapon
{
    private Guid _id;
    private string _brand;

    public Guid Id
    {
        get => _id;
        set
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Id не может быть пустым Guid.");
            _id = value;
        }
    }

    public string Brand
    {
        get => _brand;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Бренд не может быть пустым.");
            _brand = value;
        }
    }

    public Weapon(Guid id, string brand)
    {
        Id = id;
        Brand = brand;
    }
}