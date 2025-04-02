using Core.Exception;
using Core.Impl;
using Core.Interface;
using Core.Model;
using Core.Model.Users;
using Core.Service.Interface;

namespace Core.Service.Impl;

public class EmployeeService : IEmployeeService
{
    private readonly IDbService<Employee> _employeeDbService = new JsonDbService<Employee>();
    private readonly IDbService<FiredEmployee> _firedEmployeeDbService = new JsonDbService<FiredEmployee>();

    public decimal CalculateSalary(Employee employee)
    {
        throw new NotImplementedException();
    }

    public void ManageEmployeeJobRole(Employee employee, JobRole jobRole)
    {
        employee.JobRole = jobRole;
        _employeeDbService.UpdateEntity(employee.Id, employee);
    }

    public void AssignWeapon(Employee employee, Guid weaponId)
    {
        if (employee.Weapons == null)
            employee.Weapons = new List<Guid>();
        else
            employee.Weapons.Add(weaponId);
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
    
    public List<FiredEmployee> LoadFiredEmployees()
    {
        return _firedEmployeeDbService.LoadEntities();
    }

    public void FireEmployee(Guid employeeId)
    {
        var employee = _employeeDbService.LoadEntity(employeeId);
        if (employee == null) throw new NullReferenceException("Employee not found");
        var firedEmployee = new FiredEmployee(employee.Passport, employee.Id, employee.LicenseId, employee.JobRole, employee.Documents, employee.SpecialEquipments, employee.Weapons, employee.Schedule, null, null)
        {
            FiredDate = DateTime.Now,
            Reason = "Сокращение",
            Comment = ""
        };
        _firedEmployeeDbService.SaveEntity(firedEmployee);
        _employeeDbService.DeleteEntity(employeeId);
    }
}