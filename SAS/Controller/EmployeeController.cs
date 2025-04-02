using System.Collections.ObjectModel;
using Core.Impl;
using Core.Interface;
using Core.Model.Users;
using Core.Service.Impl;
using Core.Service.Interface;

namespace SAS.Controller;

public class EmployeeController : ITableController
{
    private readonly IDbService<Employee> _employeeDbService = new JsonDbService<Employee>();
    private readonly IContractService _contractService = new ContractService();
    public EmployeeController()
    {
        UpdateTable();
    }

    public ObservableCollection<Employee> Employees { get; } = [];

    public void UpdateTable()
    {
        var employees = GetEmployees();
        Employees.Clear();
        foreach (var employee in employees) Employees.Add(employee);
    }

    public ObservableCollection<Employee> GetEmployees()
    {
        return new ObservableCollection<Employee>(_employeeDbService.LoadEntities());
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
        _contractService.FindReplacementForFiredEmployee(employee.Id);
        _employeeDbService.DeleteEntity(employee.Id);
        RemoveRecord(employee);
    }

    public void UpdateEmployee(Employee employee)
    {
        _employeeDbService.UpdateEntity(employee.Id, employee);
        UpdateRecord(employee);
    }
}