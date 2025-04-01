using Core.Impl;
using Core.Interface;
using Core.Model;
using Core.Model.Users;
using Core.Service.Interface;

namespace Core.Service.Impl;

public class DutyScheduleService : IDutyScheduleService
{
    private readonly IDbService<Employee> _employeeDbService = new JsonDbService<Employee>();
    private readonly IDbService<SecuredObject> _securedObjectDbService = new JsonDbService<SecuredObject>();

    public EmployeeDutySchedule CreateDutySchedule(Guid guardId, DutySchedule schedule, Guid securingObjectId)
    {
        var guardian = _employeeDbService.LoadEntity(guardId) ??
                       throw new ArgumentException($"Employee with ID {guardId} not found");
        var securedObject = _securedObjectDbService.LoadEntity(securingObjectId) ??
                            throw new ArgumentException("Secured object not found");
        guardian.Schedule = schedule;
        guardian.SecuringObjectId = securedObject.Id;
        guardian.SecuringObjectName = securedObject.Name;
        _employeeDbService.UpdateEntity(guardId, guardian);
        return new EmployeeDutySchedule(guardian, guardian.Schedule, securingObjectId);
    }

    public EmployeeDutySchedule UpdateDutySchedule(Guid guardId, DutySchedule schedule, Guid securingObjectId)
    {
        var guardian = _employeeDbService.LoadEntity(guardId) ?? throw new ArgumentException("Employee not found");
        guardian.Schedule = schedule;
        _employeeDbService.UpdateEntity(guardId, guardian);
        return new EmployeeDutySchedule(guardian, guardian.Schedule, securingObjectId);
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
        if (guardian.Schedule == null) throw new ArgumentException("Employee does not have a schedule");

        newGuardian.Schedule = guardian.Schedule;
        newGuardian.Schedule.Replacement = new Replacement(guardId, reason);
        _employeeDbService.UpdateEntity(newEmployeeId, newGuardian);

        guardian.Schedule = null;
        _employeeDbService.UpdateEntity(guardId, guardian);
    }

    public EmployeeDutySchedule LoadDutyScheduleById(Guid guardId)
    {
        var guardian = _employeeDbService.LoadEntity(guardId) ?? throw new ArgumentException("Employee not found");
        return new EmployeeDutySchedule(guardian, guardian.Schedule!,
            guardian.SecuringObjectId ?? throw new ArgumentException("Employee does not have a schedule"));
    }

    public List<EmployeeDutySchedule> LoadAllDutySchedules()
    {
        var employees = _employeeDbService.LoadEntities();
        List<EmployeeDutySchedule> schedules = [];
        foreach (var employee in employees)
            if (employee.Schedule != null)
                schedules.Add(new EmployeeDutySchedule(employee, employee.Schedule,
                    employee.SecuringObjectId ?? throw new ArgumentException("Employee does not have a schedule")));
        return schedules;
    }
}