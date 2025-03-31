namespace Core.Model;

public class SecuredObject
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public double Area { get; set; }
    public SecurityLevel SecurityLevel { get; set; }
    public int GuardiansCount { get; set; }

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

    public SecuredObject(Guid id, string name, string address, double area, SecurityLevel securityLevel)
    {
        Id = id;
        Name = name;
        Address = address;
        Area = area;
        SecurityLevel = securityLevel;
        GuardiansCount = CalculateGuardiansCount();
    }
}

public enum SecurityLevel
{
    Low,      // Низкий уровень охраны (пл. объекта до 500кв.м)
    Medium,   // Средний уровень охраны (пл. объекта от 500кв.м до 1000кв.м)
    High,     // Высокий уровень охраны (пл. объекта от 1000кв.м до 3000кв.м)
    Hard      // Уровень повышенной охраны (пл. объекта >3000 кв.м)
}