namespace Core.Model.Users
{
    public class Employee : Person
    {
        private Guid _licenseId;
        private JobRole _jobRole;
        private Documents _documents;
        private List<Guid>? _specialEquipments;
        private List<Guid>? _weapons;
        private DutySchedule? _schedule;
        private Guid? _securingObjectId;
        private string? _securingObjectName;

        public Employee(
            Passport passport,
            Guid id,
            Guid licenseId,
            JobRole jobRole,
            Documents documents,
            List<Guid>? specialEquipments,
            List<Guid>? weapons,
            DutySchedule? schedule,
            Guid? securingObjectId,
            string? securingObjectName) : base(passport, id)
        {
            LicenseId = licenseId;
            JobRole = jobRole;
            Documents = documents;
            SpecialEquipments = specialEquipments;
            Weapons = weapons;
            Schedule = schedule;
            SecuringObjectId = securingObjectId;
            SecuringObjectName = securingObjectName;
        }

        public Guid LicenseId
        {
            get => _licenseId;
            set
            {
                if (value == Guid.Empty)
                    throw new ArgumentException("LicenseId не может быть пустым.");
                _licenseId = value;
            }
        }

        public JobRole JobRole
        {
            get => _jobRole;
            set
            {
                if (value == null)
                    throw new ArgumentException("JobRole не может быть null.");
                _jobRole = value;
            }
        }

        public Documents Documents
        {
            get => _documents;
            set
            {
                if (value == null)
                    throw new ArgumentException("Documents не может быть null.");
                _documents = value;
            }
        }

        public List<Guid>? SpecialEquipments
        {
            get => _specialEquipments;
            set => _specialEquipments = value;
        }

        public List<Guid>? Weapons
        {
            get => _weapons;
            set => _weapons = value;
        }

        public DutySchedule? Schedule
        {
            get => _schedule;
            set => _schedule = value;
        }

        public Guid? SecuringObjectId
        {
            get => _securingObjectId;
            set
            {
                if (value == Guid.Empty)
                    throw new ArgumentException("SecuringObjectId не может быть пустым Guid.");
                _securingObjectId = value;
            }
        }

        public string? SecuringObjectName
        {
            get => _securingObjectName;
            set => _securingObjectName = value;
        }
    }
}