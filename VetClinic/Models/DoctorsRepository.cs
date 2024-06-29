using System;
using System.Collections.Generic;
using System.Linq;

namespace VetClinic.Models
{
    public static class DoctorsRepository
    {
        public static void AddDoctor(VetClinicContext context, Doctor doctor)
        {
            try
            {
                context.Doctors.Add(doctor);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddDoctor: {ex.Message}");
                throw;
            }
        }

        public static List<Doctor> GetDoctors(VetClinicContext context)
        {
            try
            {
                return context.Doctors.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetDoctors: {ex.Message}");
                throw;
            }
        }

        public static Doctor GetDoctorById(VetClinicContext context, int doctorId)
        {
            try
            {
                return context.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetDoctorById: {ex.Message}");
                throw;
            }
        }

        public static void UpdateDoctor(VetClinicContext context, int doctorId, Doctor doctor)
        {
            try
            {
                var doctorToUpdate = context.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);
                if (doctorToUpdate != null)
                {
                    doctorToUpdate.Name = doctor.Name;
                    doctorToUpdate.Specialty = doctor.Specialty;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateDoctor: {ex.Message}");
                throw;
            }
        }

        public static void DeleteDoctor(VetClinicContext context, int doctorId)
        {
            try
            {
                var doctorToDelete = context.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);
                if (doctorToDelete != null)
                {
                    context.Doctors.Remove(doctorToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteDoctor: {ex.Message}");
                throw;
            }
        }
    }
}
