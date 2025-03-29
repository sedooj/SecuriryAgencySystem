namespace Core.Service.Interface;

public interface IDutyScheduleService
{
    void CreateDutySchedule(int guardId, DateTime startDate, DateTime endDate);
    void UpdateDutySchedule(int scheduleId, DateTime newStartDate, DateTime newEndDate);
    void ReplaceShift(int scheduleId, int newEmployeeId, string reason);
}