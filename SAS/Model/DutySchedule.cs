namespace SAS.Model;

public class DutySchedule(string scheduleType, Replacement? replacement, Schedule schedule)
{
    public string ScheduleType { get; set; } = scheduleType;
    public Replacement? Replacement { get; set; } = replacement;
    public Schedule Schedule { get; set; } = schedule;
    
}

public class Schedule(DateTime startDate, DateTime endDate)
{
    public DateTime StartDate { get; set; } = startDate;
    public DateTime EndDate { get; set; } = endDate;
}

public class Replacement(Guid? replacesEmployeeId, string? replacementReason)
{
    public Guid? ReplacesEmployeeId { get; set; } = replacesEmployeeId;
    public string? ReplacementReason { get; set; } = replacementReason;
}