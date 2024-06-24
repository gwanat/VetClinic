using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory
{
    public class PatientsInMemoryRepository : IPatientsRepository
    {
        private static List<Patient> _patients = new List<Patient>
        {
            new Patient { PatientId = 1, DoctorId = 5, PatientName = "Bella", Type = "Dog", Breed = "Labrador", Age = 3, OwnerInfo = "John Doe", Diagnosis = "Healthy, routine check-up" },
            new Patient { PatientId = 2, DoctorId = 1, PatientName = "Max", Type = "Dog", Breed = "German Shepherd", Age = 5, OwnerInfo = "Jane Smith", Diagnosis = "Skin allergy, medication prescribed" },
            new Patient { PatientId = 3, DoctorId = 4, PatientName = "Lucy", Type = "Dog", Breed = "Poodle", Age = 2, OwnerInfo = "Emily Johnson", Diagnosis = "Fractured leg, recovering well" },
            new Patient { PatientId = 4, DoctorId = 2, PatientName = "Charlie", Type = "Dog", Breed = "Bulldog", Age = 4, OwnerInfo = "Michael Brown", Diagnosis = "Obesity, diet plan recommended" },
            new Patient { PatientId = 5, DoctorId = 3, PatientName = "Nibbles", Type = "Hamster", Breed = "Syrian Hamster", Age = 1, OwnerInfo = "Alice White", Diagnosis = "Respiratory infection, antibiotics prescribed" },
            new Patient { PatientId = 6, DoctorId = 4, PatientName = "Tweety", Type = "Bird", Breed = "Canary", Age = 2, OwnerInfo = "Bob Green", Diagnosis = "Feather plucking, behavioral management" },
            new Patient { PatientId = 7, DoctorId = 1, PatientName = "Speedster", Type = "Other", Breed = "Sports Car", Age = 3, OwnerInfo = "Chris Blue", Diagnosis = "Engine overhaul required" },
            new Patient { PatientId = 8, DoctorId = 5, PatientName = "Thunder", Type = "Horse", Breed = "Thoroughbred", Age = 6, OwnerInfo = "Diana Black", Diagnosis = "Hoof injury, bandage and rest advised" },
            new Patient { PatientId = 9, DoctorId = 3, PatientName = "Whiskers", Type = "Hamster", Breed = "Dwarf Hamster", Age = 2, OwnerInfo = "Eve Brown", Diagnosis = "Eye infection, eye drops prescribed" },
            new Patient { PatientId = 10, DoctorId = 4, PatientName = "Polly", Type = "Bird", Breed = "Parrot", Age = 4, OwnerInfo = "Frank Yellow", Diagnosis = "Beak overgrowth, trim performed" },
            new Patient { PatientId = 11, DoctorId = 1, PatientName = "Herbie", Type = "Other", Breed = "Volkswagen Beetle", Age = 10, OwnerInfo = "George Purple", Diagnosis = "Transmission issue, parts replacement needed" },
            new Patient { PatientId = 12, DoctorId = 5, PatientName = "Majesty", Type = "Horse", Breed = "Arabian Horse", Age = 8, OwnerInfo = "Helen Silver", Diagnosis = "Colic episode, monitored closely" },
            new Patient { PatientId = 13, DoctorId = 3, PatientName = "Squeaky", Type = "Hamster", Breed = "Roborovski Hamster", Age = 1, OwnerInfo = "Ian Gold", Diagnosis = "Dehydration, rehydration therapy" },
            new Patient { PatientId = 14, DoctorId = 4, PatientName = "Sky", Type = "Bird", Breed = "Cockatiel", Age = 3, OwnerInfo = "Jack White", Diagnosis = "Wing injury, splint applied" }
        };

        private readonly IDoctorsRepository _doctorsRepository;

        public PatientsInMemoryRepository(IDoctorsRepository doctorsRepository)
        {
            _doctorsRepository = doctorsRepository;
        }

        public void AddPatient(Patient patient)
        {
            if (_patients.Any())
            {
                var maxId = _patients.Max(x => x.PatientId);
                patient.PatientId = maxId + 1;
            }
            else
            {
                patient.PatientId = 1;
            }

            _patients.Add(patient);
        }

        public List<Patient> GetPatients(bool loadDoctor = false)
        {
            if (loadDoctor)
            {
                _patients.ForEach(LoadDoctor);
            }
            return _patients;
        }

        public Patient? GetPatientById(int patientId, bool loadDoctor = false)
        {
            var patient = _patients.FirstOrDefault(x => x.PatientId == patientId);
            if (patient != null && loadDoctor)
            {
                LoadDoctor(patient);
            }
            return patient;
        }

        public void UpdatePatient(int patientId, Patient patient)
        {
            if (patientId != patient.PatientId) return;

            var patientToUpdate = _patients.FirstOrDefault(x => x.PatientId == patientId);
            if (patientToUpdate != null)
            {
                patientToUpdate.DoctorId = patient.DoctorId;
                patientToUpdate.PatientName = patient.PatientName;
                patientToUpdate.Breed = patient.Breed;
                patientToUpdate.Age = patient.Age;
                patientToUpdate.OwnerInfo = patient.OwnerInfo;
                patientToUpdate.Diagnosis = patient.Diagnosis;
                patientToUpdate.Type = patient.Type;

                LoadDoctor(patientToUpdate);
            }
        }

        public void DeletePatient(int patientId)
        {
            var patient = _patients.FirstOrDefault(x => x.PatientId == patientId);
            if (patient != null)
            {
                _patients.Remove(patient);
            }
        }

        private void LoadDoctor(Patient patient)
        {
            if (patient.DoctorId.HasValue)
            {
                patient.Doctor = _doctorsRepository.GetDoctorById(patient.DoctorId.Value);
            }
        }
    }
}
