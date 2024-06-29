using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VetClinic.Models
{
    public static class PatientsRepository
    {
        public static void AddPatient(VetClinicContext context, Patient patient)
        {
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        public static List<Patient> GetPatients(VetClinicContext context, bool loadDoctor = false)
        {
            var query = context.Patients.AsQueryable();
            if (loadDoctor)
            {
                query = query.Include(p => p.Doctor);
            }
            return query.ToList();
        }

        public static Patient GetPatientById(VetClinicContext context, int patientId, bool loadDoctor = false)
        {
            var query = context.Patients.AsQueryable();
            if (loadDoctor)
            {
                query = query.Include(p => p.Doctor);
            }
            return query.FirstOrDefault(p => p.PatientId == patientId);
        }

        public static void UpdatePatient(VetClinicContext context, int patientId, Patient patient)
        {
            var patientToUpdate = context.Patients.FirstOrDefault(p => p.PatientId == patientId);
            if (patientToUpdate != null)
            {
                patientToUpdate.DoctorId = patient.DoctorId;
                patientToUpdate.PatientName = patient.PatientName;
                patientToUpdate.Breed = patient.Breed;
                patientToUpdate.Age = patient.Age;
                patientToUpdate.OwnerInfo = patient.OwnerInfo;
                patientToUpdate.Diagnosis = patient.Diagnosis;
                patientToUpdate.Type = patient.Type;

                context.SaveChanges();
            }
        }

        public static void DeletePatient(VetClinicContext context, int patientId)
        {
            var patientToDelete = context.Patients.FirstOrDefault(p => p.PatientId == patientId);
            if (patientToDelete != null)
            {
                context.Patients.Remove(patientToDelete);
                context.SaveChanges();
            }
        }
    }
}
