using Core.Exception;

namespace Core.Model.Users;

public class Guardian : Employee
{
    private List<Guid>? _specialEquipments;

    public Guardian(Documents documents,
        List<Guid>? specialEquipments, List<Guid> weapons, DutySchedule? schedule, Guid? securingObjectId,
        Passport passport) : base(
        passport, new JobRole("Security", Role.SecurityOfficer), documents)
    {
        SpecialEquipments = specialEquipments;
        Weapons = weapons;
        Schedule = schedule;
        SecuringObjectId = securingObjectId;
    }

    public List<Guid> Weapons { get; set; }
    public DutySchedule? Schedule { get; set; }

    public Guid? SecuringObjectId { get; set; }

    public List<Guid>? SpecialEquipments
    {
        get => _specialEquipments;
        set
        {
            if (JobRole.Role == Role.SecurityOfficer)
            {
                _specialEquipments = value;
            }
            else
            {
                throw new OnlySecurityOfficerCanHaveSpecialEquipmentException();
            }
        }
    }
}