using Core.Model.Users;

namespace Core.Model;

public class EmployeeDutySchedule(Employee employee, DutySchedule duty, Guid securingObjectId)
{
    public Employee Employee { get; set; } = employee;
    public DutySchedule Duty { get; set; } = duty;
    public Guid SecuringObjectId { get; set; } = securingObjectId;
}