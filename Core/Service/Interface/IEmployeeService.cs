using Core.Model;
using Core.Model.Users;

namespace Core.Service.Interface;

public interface IEmployeeService
{
    decimal CalculateSalary(Employee employee);
    void ManageEmployeePosition(Employee employee, string position);
    void AssignWeapon(Employee employee, Weapon weapon);
    void AssignSpecialEquipment(Employee employee, Guid equipment);
}