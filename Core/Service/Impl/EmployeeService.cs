using Core.Exception;
using Core.Impl;
using Core.Interface;
using Core.Model.Users;
using Core.Service.Interface;

namespace Core.Service.Impl;

public class EmployeeService : IEmployeeService
{
    private readonly IDbService<Employee> _employeeDbService = new JsonDbService<Employee>();
    private readonly IDbService<Guardian> _guardianDbService = new JsonDbService<Guardian>();

    public decimal CalculateSalary(Employee employee)
    {
        throw new NotImplementedException();
    }

    public void ManageEmployeeJobRole(Employee employee, JobRole jobRole)
    {
        if (jobRole.Role == Role.SecurityOfficer)
        {
            var guardian = new Guardian(employee.Documents, [], [], null, null, employee.Passport);
            _guardianDbService.SaveEntity(guardian);
            _employeeDbService.DeleteEntity(employee.Id);
            return;
        }

        employee.JobRole = jobRole;
        _employeeDbService.UpdateEntity(employee.Id, employee);
    }

    public void ManageEmployeeJobRole(Guardian guardian, JobRole jobRole)
    {
        if (jobRole.Role != Role.SecurityOfficer)
        {
            var employee = new Employee(guardian.Passport, jobRole, guardian.Documents);
            _employeeDbService.SaveEntity(employee);
            _guardianDbService.DeleteEntity(guardian.Id);
            return;
        }
        guardian.JobRole = jobRole;
        _employeeDbService.UpdateEntity(guardian.Id, guardian);
    }

    public void AssignWeapon(Guardian guardian, Guid weaponId)
    {
        guardian.Weapons.Add(weaponId);
        _guardianDbService.UpdateEntity(guardian.Id, guardian);
    }

    public void AssignSpecialEquipment(Guardian guardian, Guid equipment)
    {
        var equipments = guardian.SpecialEquipments != null ? [..guardian.SpecialEquipments] : new List<Guid>();
        equipments.Add(equipment);
        guardian.SpecialEquipments = equipments;
        _guardianDbService.UpdateEntity(guardian.Id, guardian);
    }

    public void UnassignWeapon(Guardian guardian, Guid weaponId)
    {
        guardian.Weapons.Remove(weaponId);
        _guardianDbService.UpdateEntity(guardian.Id, guardian);
    }

    public void UnassignSpecialEquipment(Guardian guardian, Guid equipment)
    {
        if (guardian.SpecialEquipments == null) throw new SecurityEquipmentNullException();
        guardian.SpecialEquipments.Remove(equipment);
        _guardianDbService.UpdateEntity(guardian.Id, guardian);
    }
}