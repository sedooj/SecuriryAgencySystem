using Core.Impl;
using Core.Interface;
using Core.Model;
using Core.Model.Users;
using Core.Service.Interface;

namespace Core.Service.Impl;

public class DutyScheduleService : IDutyScheduleService
{
    private readonly IDBService<Guardian> _guardianDbService = new JsonDbService<Guardian>();


    public void CreateDutySchedule(Guid guardId, DutySchedule schedule)
    {
        var guardian = _guardianDbService.LoadEntity(guardId) ?? throw new ArgumentException("Guard not found");
        guardian.Schedule = schedule;
        _guardianDbService.UpdateEntity(guardId, guardian);
    }

    public void UpdateDutySchedule(Guid guardId, DutySchedule schedule)
    {
        var guardian = _guardianDbService.LoadEntity(guardId) ?? throw new ArgumentException("Guard not found");
        guardian.Schedule = schedule;
        _guardianDbService.UpdateEntity(guardId, guardian);
    }

    public void DeleteDutySchedule(Guid guardId)
    {
        var guardian = _guardianDbService.LoadEntity(guardId) ?? throw new ArgumentException("Guard not found");
        guardian.Schedule = null;
        _guardianDbService.UpdateEntity(guardId, guardian);
    }

    public void ReplaceShift(Guid guardId, Guid newEmployeeId, string reason)
    {
        var guardian = _guardianDbService.LoadEntity(guardId) ?? throw new ArgumentException("Guard not found");
        var newGuardian = _guardianDbService.LoadEntity(guardId) ?? throw new ArgumentException("Guard not found");
        if (guardian.Schedule == null)
        {
            throw new ArgumentException("Guard does not have a schedule");
        }
        
        newGuardian.Schedule = guardian.Schedule;
        newGuardian.Schedule.Replacement = new Replacement(guardId, reason);
        _guardianDbService.UpdateEntity(newEmployeeId, newGuardian);
        
        guardian.Schedule = null;
        _guardianDbService.UpdateEntity(guardId, guardian);
    }
}