namespace VetClinic.Models
{
    public static class DoctorsRepository
    {
        private static List<Doctor> _doctors = new List<Doctor>()
        {
            new Doctor { DoctorId = 1, Name = "Dr. Gregory House", Description = "Diagnostic Medicine" },
            new Doctor { DoctorId = 2, Name = "Dr. Meredith Grey", Description = "General Surgery" },
            new Doctor { DoctorId = 3, Name = "Dr. John Watson", Description = "General Practice" },
            new Doctor { DoctorId = 4, Name = "Dr. Stephen Strange", Description = "Neurosurgery" },
            new Doctor { DoctorId = 5, Name = "Dr. Miranda Bailey", Description = "General Surgery" }
        };

        public static void AddDoctor(Doctor doctor)
        {
            if (_doctors != null && _doctors.Count > 0)
            {
                var maxId = _doctors.Max(x => x.DoctorId);
                doctor.DoctorId = maxId + 1;
            }
            else
            {
                doctor.DoctorId = 1;
            }
            if (_doctors == null) _doctors = new List<Doctor>();
            _doctors.Add(doctor);
        }

        public static List<Doctor> GetDoctors() => _doctors;

        public static Doctor? GetDoctorById(int doctorId)
        {
            var doctor = _doctors.FirstOrDefault(x => x.DoctorId == doctorId);
            if (doctor != null)
            {
                return new Doctor
                {
                    DoctorId = doctor.DoctorId,
                    Name = doctor.Name,
                    Description = doctor.Description,
                };
            }

            return null;
        }

        public static void UpdateDoctor(int doctorId, Doctor doctor)
        {
            if (doctorId != doctor.DoctorId) return;

            var doctorToUpdate = _doctors.FirstOrDefault(x => x.DoctorId == doctorId);
            if (doctorToUpdate != null)
            {
                doctorToUpdate.Name = doctor.Name;
                doctorToUpdate.Description = doctor.Description;
            }
        }

        public static void DeleteDoctor(int doctorId)
        {
            var doctor = _doctors.FirstOrDefault(x => x.DoctorId == doctorId);
            if (doctor != null)
            {
                _doctors.Remove(doctor);
            }
        }
    }
}