namespace VetClinic.Models
{
    public class PatientsRepository
    {
        private static List<Patient> _patients = new List<Patient>()
        {
            new Patient { PatientId = 1, DoctorId = 5, PatientName = "Bella", Breed = "Labrador", Age = 3, OwnerInfo = "John Doe", Diagnosis = "Healthy, routine check-up" },
            new Patient { PatientId = 2, DoctorId = 1, PatientName = "Max", Breed = "German Shepherd", Age = 5, OwnerInfo = "Jane Smith", Diagnosis = "Skin allergy, medication prescribed" },
            new Patient { PatientId = 3, DoctorId = 4, PatientName = "Lucy", Breed = "Poodle", Age = 2, OwnerInfo = "Emily Johnson", Diagnosis = "Fractured leg, recovering well" },
            new Patient { PatientId = 4, DoctorId = 2, PatientName = "Charlie", Breed = "Bulldog", Age = 4, OwnerInfo = "Michael Brown", Diagnosis = "Obesity, diet plan recommended" },
            new Patient { PatientId = 5, DoctorId = 3, PatientName = "Nibbles", Breed = "Syrian Hamster", Age = 1, OwnerInfo = "Alice White", Diagnosis = "Respiratory infection, antibiotics prescribed" },
            new Patient { PatientId = 6, DoctorId = 4, PatientName = "Tweety", Breed = "Canary", Age = 2, OwnerInfo = "Bob Green", Diagnosis = "Feather plucking, behavioral management" },
            new Patient { PatientId = 7, DoctorId = 1, PatientName = "Speedster", Breed = "Sports Car", Age = 3, OwnerInfo = "Chris Blue", Diagnosis = "Engine overhaul required" },
            new Patient { PatientId = 8, DoctorId = 5, PatientName = "Thunder", Breed = "Thoroughbred", Age = 6, OwnerInfo = "Diana Black", Diagnosis = "Hoof injury, bandage and rest advised" },
            new Patient { PatientId = 9, DoctorId = 3, PatientName = "Whiskers", Breed = "Dwarf Hamster", Age = 2, OwnerInfo = "Eve Brown", Diagnosis = "Eye infection, eye drops prescribed" },
            new Patient { PatientId = 10, DoctorId = 4, PatientName = "Polly", Breed = "Parrot", Age = 4, OwnerInfo = "Frank Yellow", Diagnosis = "Beak overgrowth, trim performed" },
            new Patient { PatientId = 11, DoctorId = 1, PatientName = "Herbie", Breed = "Volkswagen Beetle", Age = 10, OwnerInfo = "George Purple", Diagnosis = "Transmission issue, parts replacement needed" },
            new Patient { PatientId = 12, DoctorId = 5, PatientName = "Majesty", Breed = "Arabian Horse", Age = 8, OwnerInfo = "Helen Silver", Diagnosis = "Colic episode, monitored closely" },
            new Patient { PatientId = 13, DoctorId = 3, PatientName = "Squeaky", Breed = "Roborovski Hamster", Age = 1, OwnerInfo = "Ian Gold", Diagnosis = "Dehydration, rehydration therapy" },
            new Patient { PatientId = 14, DoctorId = 4, PatientName = "Sky", Breed = "Cockatiel", Age = 3, OwnerInfo = "Jack White", Diagnosis = "Wing injury, splint applied" }
        };

        public static void AddPatient(Patient patient)
        {
            if (_patients != null && _patients.Count > 0)
            {
                var maxId = _patients.Max(x => x.PatientId);
                patient.PatientId = maxId + 1;
            }
            else
            {
                patient.PatientId = 1;
            }

            if (_patients == null) _patients = new List<Patient>();
            _patients.Add(patient);
        }

        public static List<Patient> GetPatients(bool loadDoctor = false)
        {
            if (!loadDoctor)
            {
                return _patients;
            }
            else
            {
                if (_patients != null && _patients.Count > 0)
                {
                    _patients.ForEach(x =>
                    {
                        if (x.DoctorId.HasValue)
                            x.Doctor = DoctorsRepository.GetDoctorById(x.DoctorId.Value);
                    });
                }
                return _patients ?? new List<Patient>();
            }
        }

        public static Patient? GetPatientById(int patientId, bool loadDoctor = false)
        {
            var patient = _patients.FirstOrDefault(x => x.PatientId == patientId);
            if (patient != null)
            {
                var pat = new Patient
                {
                    PatientId = patient.PatientId,
                    DoctorId = patient.DoctorId,
                    PatientName = patient.PatientName,
                    Breed = patient.Breed,
                    Age = patient.Age,
                    OwnerInfo = patient.OwnerInfo,
                    Diagnosis = patient.Diagnosis
                };

                if (loadDoctor && pat.DoctorId.HasValue)
                {
                    patient.Doctor = DoctorsRepository.GetDoctorById(pat.DoctorId.Value);
                }
                return pat;
            }
            return null;
        }

        public static void UpdatePatient(int patientId, Patient patient)
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
            }
        }

        public static void DeletePatient(int patientId)
        {
            var patient = _patients.FirstOrDefault(x => x.PatientId == patientId);
            if (patient != null)
            {
                _patients.Remove(patient);
            }
        }
    }
}
