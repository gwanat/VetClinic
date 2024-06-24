namespace VetClinic.Models
{
    public static class DoctorsRepository
    {
        private static List<Doctor> _doctors = new List<Doctor>()
        {
            new Doctor { DoctorId = 1, Name = "Dr. Gregory House", Specialty = "Diagnostic Medicine" },
            new Doctor { DoctorId = 2, Name = "Dr. Meredith Grey", Specialty = "General Surgery" },
            new Doctor { DoctorId = 3, Name = "Dr. John Watson", Specialty = "General Practice" },
            new Doctor { DoctorId = 4, Name = "Dr. Stephen Strange", Specialty = "Neurosurgery" },
            new Doctor { DoctorId = 5, Name = "Dr. Miranda Bailey", Specialty = "General Surgery" }
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
                    Specialty = doctor.Specialty,
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
                doctorToUpdate.Specialty = doctor.Specialty;
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