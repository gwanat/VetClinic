using CoreBusiness;
using UseCases.DataStorePluginInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace Plugins.DataStore.InMemory
{
    public class DoctorsInMemoryRepository : IDoctorsRepository
    {
        private static List<Doctor> _doctors = new List<Doctor>()
        {
            new Doctor { DoctorId = 1, Name = "Dr. Gregory House", Specialty = "Diagnostic Medicine" },
            new Doctor { DoctorId = 2, Name = "Dr. Meredith Grey", Specialty = "General Surgery" },
            new Doctor { DoctorId = 3, Name = "Dr. John Watson", Specialty = "General Practice" },
            new Doctor { DoctorId = 4, Name = "Dr. Stephen Strange", Specialty = "Neurosurgery" },
            new Doctor { DoctorId = 5, Name = "Dr. Miranda Bailey", Specialty = "General Surgery" }
        };

        public void AddDoctor(Doctor doctor)
        {
            if (_doctors.Any())
            {
                var maxId = _doctors.Max(x => x.DoctorId);
                doctor.DoctorId = maxId + 1;
            }
            else
            {
                doctor.DoctorId = 1;
            }

            _doctors.Add(doctor);
        }

        public List<Doctor> GetDoctors() => _doctors;

        public Doctor? GetDoctorById(int doctorId)
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

        public void UpdateDoctor(int doctorId, Doctor doctor)
        {
            if (doctorId != doctor.DoctorId) return;

            var doctorToUpdate = _doctors.FirstOrDefault(x => x.DoctorId == doctorId);
            if (doctorToUpdate != null)
            {
                doctorToUpdate.Name = doctor.Name;
                doctorToUpdate.Specialty = doctor.Specialty;
            }
        }

        public void DeleteDoctor(int doctorId)
        {
            var doctor = _doctors.FirstOrDefault(x => x.DoctorId == doctorId);
            if (doctor != null)
            {
                _doctors.Remove(doctor);
            }
        }

        IEnumerable<Doctor> IDoctorsRepository.GetDoctors()
        {
            throw new NotImplementedException();
        }
    }
}
