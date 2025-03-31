using System.Collections.ObjectModel;
using System.Diagnostics;
using Core.Impl;
using Core.Interface;
using Core.Model.Users;

namespace SAS.Controller;

public class EmployeeController : ITableController
{
    private readonly IDbService<Employee> _employeeDbService = new JsonDbService<Employee>();
    public ObservableCollection<Employee> Employees { get; private set; } = [];

    public EmployeeController()
    {
        UpdateTable();
    }

    public ObservableCollection<Employee> GetEmployees()
    {
        return new ObservableCollection<Employee>(_employeeDbService.LoadEntities());
    }

    public void UpdateTable()
    {
        var employees = GetEmployees();
        Employees.Clear();
        foreach (var employee in employees)
        {
            Employees.Add(employee);
        }
    }

    private void AddRecord(Employee employee)
    {
        Employees.Add(employee);
    }

    private void RemoveRecord(Employee employee)
    {
        Employees.Remove(employee);
    }

    private void UpdateRecord(Employee employee)
    {
        var index = Employees.IndexOf(employee);
        if (index == -1) return;
        Employees[index] = employee;
    }

    public void AddEmployee(Employee employee)
    {
        _employeeDbService.SaveEntity(employee);
        AddRecord(employee);
    }

    public void RemoveEmployee(Employee employee)
    {
        _employeeDbService.DeleteEntity(employee.EmployeeId);
        RemoveRecord(employee);
    }

    public void UpdateEmployee(Employee employee)
    {
        _employeeDbService.UpdateEntity(employee.EmployeeId, employee);
        UpdateRecord(employee);
    }
}