using Core.Exception;
using Core.Impl;
using Core.Interface;
using Core.Model.Users;
using Core.Service.Interface;

namespace Core.Service.Impl;

public class EmployeeService : IEmployeeService
{
    private readonly IDbService<Employee> _employeeDbService = new JsonDbService<Employee>();

    public decimal CalculateSalary(Employee employee)
    {
        throw new NotImplementedException();
    }

    public void ManageEmployeeJobRole(Employee employee, JobRole jobRole)
    {
        if (jobRole.Role == Role.SecurityOfficer)
        {
            var guardian = new Employee(employee.Passport, jobRole, employee.Documents);
            _employeeDbService.SaveEntity(guardian);
            _employeeDbService.DeleteEntity(employee.Id);
            return;
        }

        employee.JobRole = jobRole;
        _employeeDbService.UpdateEntity(employee.Id, employee);
    }

    public void AssignWeapon(Employee employee, Guid weaponId)
    {
        if (employee.Weapons == null)
        {
            employee.Weapons = new List<Guid>();
        }
        else
        {
            employee.Weapons.Add(weaponId);
        }
        _employeeDbService.UpdateEntity(employee.Id, employee);
    }

    public void AssignSpecialEquipment(Employee employee, Guid equipment)
    {
        var equipments = employee.SpecialEquipments != null ? [..employee.SpecialEquipments] : new List<Guid>();
        equipments.Add(equipment);
        employee.SpecialEquipments = equipments;
        _employeeDbService.UpdateEntity(employee.Id, employee);
    }

    public void UnassignWeapon(Employee employee, Guid weaponId)
    {
        if (employee.Weapons == null) throw new SecurityEquipmentNullException();
        employee.Weapons.Remove(weaponId);
        _employeeDbService.UpdateEntity(employee.Id, employee);
    }

    public void UnassignSpecialEquipment(Employee employee, Guid equipment)
    {
        if (employee.SpecialEquipments == null) throw new SecurityEquipmentNullException();
        employee.SpecialEquipments.Remove(equipment);
        _employeeDbService.UpdateEntity(employee.Id, employee);
    }
}