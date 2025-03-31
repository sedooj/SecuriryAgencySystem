using Core.Impl;
using Core.Interface;
using Core.Model;
using Core.Model.Users;
using Core.Service.Interface;

namespace Core.Service.Impl;

public class DutyScheduleService : IDutyScheduleService
{
    private readonly IDbService<Employee> _employeeDbService = new JsonDbService<Employee>();


    public void CreateDutySchedule(Guid guardId, DutySchedule schedule)
    {
        var guardian = _employeeDbService.LoadEntity(guardId) ?? throw new ArgumentException("Employee not found");
        guardian.Schedule = schedule;
        _employeeDbService.UpdateEntity(guardId, guardian);
    }

    public void UpdateDutySchedule(Guid guardId, DutySchedule schedule)
    {
        var guardian = _employeeDbService.LoadEntity(guardId) ?? throw new ArgumentException("Employee not found");
        guardian.Schedule = schedule;
        _employeeDbService.UpdateEntity(guardId, guardian);
    }

    public void DeleteDutySchedule(Guid guardId)
    {
        var guardian = _employeeDbService.LoadEntity(guardId) ?? throw new ArgumentException("Employee not found");
        guardian.Schedule = null;
        _employeeDbService.UpdateEntity(guardId, guardian);
    }

    public void ReplaceShift(Guid guardId, Guid newEmployeeId, string reason)
    {
        var guardian = _employeeDbService.LoadEntity(guardId) ?? throw new ArgumentException("Employee not found");
        var newGuardian = _employeeDbService.LoadEntity(guardId) ?? throw new ArgumentException("Employee not found");
        if (guardian.Schedule == null)
        {
            throw new ArgumentException("Employee does not have a schedule");
        }
        
        newGuardian.Schedule = guardian.Schedule;
        newGuardian.Schedule.Replacement = new Replacement(guardId, reason);
        _employeeDbService.UpdateEntity(newEmployeeId, newGuardian);
        
        guardian.Schedule = null;
        _employeeDbService.UpdateEntity(guardId, guardian);
    }
}