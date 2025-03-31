namespace Core.Model;

public class JobRole(string position, Role role)
{
    public string Position { get; set; } = position;
    public Role Role { get; set; } = role;
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