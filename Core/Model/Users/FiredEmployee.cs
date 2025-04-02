namespace Core.Model.Users;

public class FiredEmployee(
    Passport passport,
    Guid id,
    Guid licenseId,
    JobRole jobRole,
    Documents documents,
    List<Guid>? specialEquipments,
    List<Guid>? weapons,
    DutySchedule? schedule,
    Guid? securingObjectId,
    string? securingObjectName)
    : Employee(passport, id, licenseId, jobRole, documents, specialEquipments, weapons, schedule, securingObjectId,
        securingObjectName)
{
    public DateTime FiredDate { get; set; }
    public string? Reason { get; set; }
    public string? Comment { get; set; }
}