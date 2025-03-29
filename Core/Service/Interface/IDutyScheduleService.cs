using Core.Model;

namespace Core.Service.Interface;

public interface IDutyScheduleService
{
    void CreateDutySchedule(Guid guardId, DutySchedule schedule);
    void UpdateDutySchedule(Guid guardId, DutySchedule schedule);
    void DeleteDutySchedule(Guid guardId);
    void ReplaceShift(Guid guardId, Guid newEmployeeId, string reason);
}