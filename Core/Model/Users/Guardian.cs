using Core.Exception;

namespace Core.Model.Users;

public class Guardian : Employee
{
    private List<Guid>? _specialEquipmentsIds;

    public Guardian(Documents documents,
        List<Guid>? specialEquipmentsIds, List<Weapon> weapons, DutySchedule? schedule, Guid? securingObjectId,
        Passport passport) : base(
        passport, new JobRole("Security", Role.SecurityOfficer), documents)
    {
        SpecialEquipmentsIds = specialEquipmentsIds;
        Weapons = weapons;
        Schedule = schedule;
        SecuringObjectId = securingObjectId;
    }

    public List<Weapon> Weapons { get; set; }
    public DutySchedule? Schedule { get; set; }

    public Guid? SecuringObjectId { get; set; }

    public List<Guid>? SpecialEquipmentsIds
    {
        get => _specialEquipmentsIds;
        set
        {
            if (JobRole.Role == Role.SecurityOfficer)
            {
                _specialEquipmentsIds = value;
            }
            else
            {
                throw new OnlySecurityOfficerCanHaveSpecialEquipmentException();
            }
        }
    }
}