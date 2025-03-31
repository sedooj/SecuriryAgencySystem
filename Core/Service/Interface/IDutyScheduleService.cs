using Core.Model;

namespace Core.Service.Interface;

public interface IDutyScheduleService
{
    EmployeeDutySchedule CreateDutySchedule(Guid guardId, DutySchedule schedule, Guid securingObjectId);
    EmployeeDutySchedule UpdateDutySchedule(Guid guardId, DutySchedule schedule, Guid securingObjectId);
    void DeleteDutySchedule(Guid guardId);
    void ReplaceShift(Guid guardId, Guid newEmployeeId, string reason);
    EmployeeDutySchedule LoadDutyScheduleById(Guid guardId);
    List<EmployeeDutySchedule> LoadAllDutySchedules();
}