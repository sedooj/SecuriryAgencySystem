namespace Core.Model.Users;

public class Employee(
    Passport passport,
    JobRole jobRole,
    Documents documents,
    List<Guid>? specialEquipments = null,
    List<Guid>? weapons = null,
    DutySchedule? schedule = null,
    Guid? securingObjectId = null)
    : Person(passport)
{
    public Guid LicenseId { get; } = Guid.NewGuid();
    public Guid EmployeeId { get; } = Guid.NewGuid();
    public JobRole JobRole { get; set; } = jobRole;
    public Documents Documents { get; set; } = documents;
    public List<Guid>? SpecialEquipments { get; set; } = specialEquipments;
    public List<Guid>? Weapons { get; set; } = weapons;
    public DutySchedule? Schedule { get; set; } = schedule;
    public Guid? SecuringObjectId { get; set; } = securingObjectId;
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