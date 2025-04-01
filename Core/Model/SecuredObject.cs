namespace Core.Model;

public class SecuredObject
{
    private Guid _id;
    private string _name;
    private string _address;
    private double _area;
    private SecurityLevel _securityLevel;
    private int _guardiansCount;
    private Guid? _ownerId;
    private OwnerType? _ownerType;

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

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 1 || value.Length > 100)
                throw new ArgumentException("Название должно быть от 1 до 100 символов.");
            _name = value;
        }
    }

    public string Address
    {
        get => _address;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 1 || value.Length > 200)
                throw new ArgumentException("Адрес дол��ен быть от 1 до 200 символов.");
            _address = value;
        }
    }

    public double Area
    {
        get => _area;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Площадь должна быть больше 0.");
            _area = value;
        }
    }

    public SecurityLevel SecurityLevel
    {
        get => _securityLevel;
        set => _securityLevel = value;
    }

    public int GuardiansCount
    {
        get => _guardiansCount;
        set
        {
            if (value < 1)
                throw new ArgumentException("Количество охранников должно быть больше 0.");
            _guardiansCount = value;
        }
    }

    public Guid? OwnerId
    {
        get => _ownerId;
        set => _ownerId = value;
    }

    public OwnerType? OwnerType
    {
        get => _ownerType;
        set => _ownerType = value;
    }

    private int CalculateGuardiansCount()
    {
        return SecurityLevel switch
        {
            SecurityLevel.Low => 1,
            SecurityLevel.Medium => 2,
            SecurityLevel.High => 4,
            SecurityLevel.Hard => 6,
            _ => 1
        };
    }

    public SecuredObject(Guid id, string name, string address, double area, SecurityLevel securityLevel, Guid? ownerId, OwnerType? ownerType)
    {
        Id = id;
        Name = name;
        Address = address;
        Area = area;
        SecurityLevel = securityLevel;
        GuardiansCount = CalculateGuardiansCount();
        OwnerId = ownerId;
        OwnerType = ownerType;
    }
}

public enum SecurityLevel
{
    Low,      // Низкий уровень охраны (пл. объекта до 500кв.м)
    Medium,   // Средний уровень охраны (пл. объекта от 500кв.м до 1000кв.м)
    High,     // Высокий уровень охраны (пл. объекта от 1000кв.м до 3000кв.м)
    Hard      // Уровень повышенной охраны (пл. объекта >3000 кв.м)
}

public enum OwnerType
{
    Individual,
    Corp
}