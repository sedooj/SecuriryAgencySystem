namespace SecurityAgencySystem.Model;

public class DutySchedule(string scheduleType, Guid? replacesEmployeeId, string schedule)
{
    public string ScheduleType { get; set; } = scheduleType;

    public Guid? ReplacesEmployeeId { get; set; } = replacesEmployeeId;

    public string Schedule { get; set; } = schedule;
}