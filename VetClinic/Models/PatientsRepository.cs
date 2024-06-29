using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VetClinic.Models
{
    public static class PatientsRepository
    {
        public static void AddPatient(VetClinicContext context, Patient patient)
        {
            try
            {
                context.Patients.Add(patient);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddPatient: {ex.Message}");
                throw;
            }
        }

        public static List<Patient> GetPatients(VetClinicContext context, bool loadDoctor = false)
        {
            try
            {
                var query = context.Patients.AsQueryable();
                if (loadDoctor)
                {
                    query = query.Include(p => p.Doctor);
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetPatients: {ex.Message}");
                throw;
            }
        }

        public static Patient GetPatientById(VetClinicContext context, int patientId, bool loadDoctor = false)
        {
            try
            {
                var query = context.Patients.AsQueryable();
                if (loadDoctor)
                {
                    query = query.Include(p => p.Doctor);
                }
                return query.FirstOrDefault(p => p.PatientId == patientId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetPatientById: {ex.Message}");
                throw;
            }
        }

        public static void UpdatePatient(VetClinicContext context, int patientId, Patient patient)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdatePatient: {ex.Message}");
                throw;
            }
        }

        public static void DeletePatient(VetClinicContext context, int patientId)
        {
            try
            {
                var patientToDelete = context.Patients.FirstOrDefault(p => p.PatientId == patientId);
                if (patientToDelete != null)
                {
                    context.Patients.Remove(patientToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeletePatient: {ex.Message}");
                throw;
            }
        }
    }
}
