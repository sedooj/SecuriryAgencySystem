namespace Core.Model;

public class Employee(
    string name,
    string surname,
    string patronymic,
    
    string position,
    decimal salary,
    List<Weapon> weapons,
    List<Guid> specialEquipmentsIds,
    DutySchedule dutySchedule,
    Documents documents) : Person(name, surname, patronymic)
{
    public Guid LicenseId { get; } = Guid.NewGuid();

    public Guid EmployeeId { get; } = Guid.NewGuid();

    public string Position { get; set; } = position;

    public decimal Salary { get; set; } = salary;

    public List<Weapon> Weapons { get; set; } = weapons;

    public List<Guid> SpecialEquipmentsIds { get; set; } = specialEquipmentsIds;

    public DutySchedule Schedule { get; set; } = dutySchedule;

    public Documents Documents { get; set; } = documents;
}