using System;
using Core.Model.Users;

namespace Core.Model
{
    public class EmployeeDutySchedule
    {
        private Employee _employee;
        private DutySchedule _duty;
        private Guid _securingObjectId;

        public EmployeeDutySchedule(Employee employee, DutySchedule duty, Guid securingObjectId)
        {
            Employee = employee;
            Duty = duty;
            SecuringObjectId = securingObjectId;
        }

        public Employee Employee
        {
            get => _employee;
            set
            {
                if (value == null)
                    throw new ArgumentException("Employee не может быть null.");
                _employee = value;
            }
        }

        public DutySchedule Duty
        {
            get => _duty;
            set
            {
                if (value == null)
                    throw new ArgumentException("Duty не может быть null.");
                _duty = value;
            }
        }

        public Guid SecuringObjectId
        {
            get => _securingObjectId;
            set
            {
                if (value == Guid.Empty)
                    throw new ArgumentException("SecuringObjectId не может быть пустым Guid.");
                _securingObjectId = value;
            }
        }
    }
}