namespace Core.Model.Users;

public class Employee : Person
{
    private Documents _documents;
    private JobRole _jobRole;
    private Guid _licenseId;
    private Guid? _securingObjectId;

    public Employee(
        Passport passport,
        Guid id,
        Guid licenseId,
        JobRole jobRole,
        Documents documents,
        List<Guid>? specialEquipments,
        List<Guid>? weapons,
        DutySchedule? schedule,
        Guid? securingObjectId,
        string? securingObjectName) : base(passport, id)
    {
        LicenseId = licenseId;
        JobRole = jobRole;
        Documents = documents;
        SpecialEquipments = specialEquipments;
        Weapons = weapons;
        Schedule = schedule;
        SecuringObjectId = securingObjectId;
        SecuringObjectName = securingObjectName;
    }

    public Guid LicenseId
    {
        get => _licenseId;
        set
        {
            if (value == Guid.Empty)
                throw new ArgumentException("LicenseId не может быть пустым.");
            _licenseId = value;
        }
    }

    public JobRole JobRole
    {
        get => _jobRole;
        set
        {
            if (value == null)
                throw new ArgumentException("JobRole не может быть null.");
            _jobRole = value;
        }
    }

    public Documents Documents
    {
        get => _documents;
        set
        {
            if (value == null)
                throw new ArgumentException("Documents не может быть null.");
            _documents = value;
        }
    }

    public List<Guid>? SpecialEquipments { get; set; }

    public List<Guid>? Weapons { get; set; }

    public DutySchedule? Schedule { get; set; }

    public Guid? SecuringObjectId
    {
        get => _securingObjectId;
        set
        {
            if (value == Guid.Empty)
                throw new ArgumentException("SecuringObjectId не может быть пустым Guid.");
            _securingObjectId = value;
        }
    }

    public string? SecuringObjectName { get; set; }
}