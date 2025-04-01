namespace Core.Model;

public class JobRole
{
    private string _position;
    private Role _role;

    public JobRole(string position, Role role)
    {
        Position = position;
        Role = role;
    }

    public string Position
    {
        get => _position;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 1 || value.Length > 100)
                throw new ArgumentException("Название должности должно быть длиной от 1 до 100 символов.");
            _position = value;
        }
    }

    public Role Role
    {
        get => _role;
        set
        {
            if (!Enum.IsDefined(typeof(Role), value))
                throw new ArgumentException("Недопустимая роль.");
            _role = value;
        }
    }

    public decimal Salary => GetSalaryByRole(Role);

    private decimal GetSalaryByRole(Role employeeRole)
    {
        return employeeRole switch
        {
            Role.SecurityOfficer => 50000,
            Role.Cleaner => 32000,
            Role.Manager => 70000,
            Role.Director => 90000,
            _ => 0
        };
    }
}

public enum Role
{
    SecurityOfficer,
    Cleaner,
    Manager,
    Director
}