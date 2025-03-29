using SAS.Model;
using SecurityAgencySystem.Model;

namespace SAS.Service.Core;

public interface IEmployeeService
{
    decimal CalculateSalary(Employee employee);
    void ManageEmployeePosition(Employee employee, string position);
    void AssignWeapon(Employee employee, Weapon weapon);
    void AssignSpecialEquipment(Employee employee, Guid equipment);
}