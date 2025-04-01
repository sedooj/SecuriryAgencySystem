namespace Core.Model
{
    public class DutySchedule
    {
        private string _scheduleType;
        private Replacement? _replacement;
        private Schedule _schedule;

        public DutySchedule(string scheduleType, Replacement? replacement, Schedule schedule)
        {
            ScheduleType = scheduleType;
            Replacement = replacement;
            Schedule = schedule;
        }

        public string ScheduleType
        {
            get => _scheduleType;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 1 || value.Length > 50)
                    throw new ArgumentException("Тип расписания должен быть от 1 до 50 символов.");
                _scheduleType = value;
            }
        }

        public Replacement? Replacement
        {
            get => _replacement;
            set => _replacement = value;
        }

        public Schedule Schedule
        {
            get => _schedule;
            set
            {
                if (value == null)
                    throw new ArgumentException("Schedule не может быть null.");
                _schedule = value;
            }
        }
    }

    public class Schedule
    {
        private DateTime _startDate;
        private DateTime _endDate;

        public Schedule(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value == default)
                    throw new ArgumentException("StartDate не может быть пустым.");
                _startDate = value;
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value == default)
                    throw new ArgumentException("EndDate не может быть пустым.");
                _endDate = value;
            }
        }

        public string WorkingDate => $"{StartDate.ToShortDateString()} - {EndDate.ToShortDateString()}";
        public string WorkingTime => $"{StartDate.ToShortTimeString()} - {EndDate.ToShortTimeString()}";
    }

    public class Replacement
    {
        private Guid? _replacesEmployeeId;
        private string? _replacementReason;

        public Replacement(Guid? replacesEmployeeId, string? replacementReason)
        {
            ReplacesEmployeeId = replacesEmployeeId;
            ReplacementReason = replacementReason;
        }

        public Guid? ReplacesEmployeeId
        {
            get => _replacesEmployeeId;
            set => _replacesEmployeeId = value;
        }

        public string? ReplacementReason
        {
            get => _replacementReason;
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentException("Причина замены не должна быть длиннее 200 символов.");
                _replacementReason = value;
            }
        }
    }
}