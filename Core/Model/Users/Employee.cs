namespace Core.Model.Users;

public class Employee : Person
{
    public Employee(
        Passport passport,
        JobRole jobRole,
        Documents documents) : base(passport)
    {
        JobRole = jobRole;
        Documents = documents;
    }

    public Guid LicenseId { get; } = Guid.NewGuid();
    public Guid EmployeeId { get; } = Guid.NewGuid();
    public JobRole JobRole { get; set; }
    public Documents Documents { get; set; }
    
}

public class JobRole
{
    public JobRole(string position, Role role)
    {
        Position = position;
        Role = role;
    }

    public string Position { get; set; }
    public Role Role { get; set; }
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