using Core.Model.Users;

namespace Core.Service.Interface;

public interface IEmployeeService
{
    decimal CalculateSalary(Employee employee);
    void ManageEmployeeJobRole(Employee employee, JobRole jobRole);
    void AssignWeapon(Employee employee, Guid weaponId);
    void AssignSpecialEquipment(Employee employee, Guid equipment);
    void UnassignWeapon(Employee employee, Guid weaponId);
    void UnassignSpecialEquipment(Employee employee, Guid equipment);
}