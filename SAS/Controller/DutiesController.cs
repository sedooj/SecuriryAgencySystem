using System.Collections.ObjectModel;
using System.Diagnostics;
using Core.Impl;
using Core.Service.Interface;
using Core.Model;
using Core.Model.Users;
using Core.Service.Impl;

namespace SAS.Controller;

public class DutiesController : ITableController
{
    private readonly IDutyScheduleService _dutyScheduleService = new DutyScheduleService();

    public ObservableCollection<EmployeeDutySchedule> EmployeeDutySchedules { get; private set; } = [];

    public DutiesController()
    {
        UpdateTable();
    }
    
    public List<Employee> GetAllEmployees()
    {
        var employeeService = new JsonDbService<Employee>();
        return employeeService.LoadEntities();
    }
    
    public List<EmployeeDutySchedule> GetAllDuties()
    {
        return _dutyScheduleService.LoadAllDutySchedules();
    }

    public EmployeeDutySchedule GetDutyById(Guid id)
    {
        return _dutyScheduleService.LoadDutyScheduleById(id);
    }

    public void AddDuty(Guid guardId, DutySchedule duty, Guid securingObjectId)
    {
        _dutyScheduleService.CreateDutySchedule(guardId, duty, securingObjectId);
        UpdateTable();
    }

    public void UpdateDuty(Guid guardId, DutySchedule updatedDuty, Guid securingObjectId)
    {
        _dutyScheduleService.UpdateDutySchedule(guardId, updatedDuty, securingObjectId);
        UpdateTable();
    }

    public void DeleteDuty(Guid guardId)
    {
        _dutyScheduleService.DeleteDutySchedule(guardId);
        UpdateTable();
    }

    private ObservableCollection<EmployeeDutySchedule> GetEmployeeDutySchedules()
    {
        return new ObservableCollection<EmployeeDutySchedule>(GetAllDuties());
    }

    public void UpdateTable()
    {
        var duties = GetEmployeeDutySchedules();
        EmployeeDutySchedules.Clear();
        foreach (var duty in duties)
        {
            EmployeeDutySchedules.Add(duty);
        }
    }
}