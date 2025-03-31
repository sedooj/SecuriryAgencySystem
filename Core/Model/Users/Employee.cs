namespace Core.Model.Users;

public class Employee : Person
{
    public Employee(
        Passport passport,
        Guid id,
        Guid licenseId,
        Guid employeeId,
        JobRole jobRole,
        Documents documents,
        List<Guid>? specialEquipments,
        List<Guid>? weapons,
        DutySchedule? schedule,
        Guid? securingObjectId,
        string? securingObjectName) : base(passport, id)
    {
        LicenseId = licenseId;
        EmployeeId = employeeId;
        JobRole = jobRole;
        Documents = documents;
        SpecialEquipments = specialEquipments;
        Weapons = weapons;
        Schedule = schedule;
        SecuringObjectId = securingObjectId;
        SecuringObjectName = securingObjectName;
    }

    public Guid LicenseId { get; set; }
    public Guid EmployeeId { get; set; }
    public JobRole JobRole { get; set; }
    public Documents Documents { get; set; }
    public List<Guid>? SpecialEquipments { get; set; }
    public List<Guid>? Weapons { get; set; }
    public DutySchedule? Schedule { get; set; }
    public Guid? SecuringObjectId { get; set; }
    public string? SecuringObjectName { get; set; }
}