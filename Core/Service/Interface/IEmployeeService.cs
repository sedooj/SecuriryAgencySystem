using Core.Model;
using Core.Model.Users;

namespace Core.Service.Interface;

public interface IEmployeeService
{
    decimal CalculateSalary(Employee employee);
    void ManageEmployeeJobRole(Employee employee, JobRole jobRole);
    void ManageEmployeeJobRole(Guardian guardian, JobRole jobRole);
    void AssignWeapon(Guardian guardian, Guid weaponId);
    void AssignSpecialEquipment(Guardian guardian, Guid equipment);
    void UnassignWeapon(Guardian guardian, Guid weaponId);
    void UnassignSpecialEquipment(Guardian guardian, Guid equipment);
}