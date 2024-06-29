using System.Collections.Generic;
using System.Linq;

namespace VetClinic.Models
{
    public static class DoctorsRepository
    {
        public static void AddDoctor(VetClinicContext context, Doctor doctor)
        {
            context.Doctors.Add(doctor);
            context.SaveChanges();
        }

        public static List<Doctor> GetDoctors(VetClinicContext context)
        {
            return context.Doctors.ToList();
        }

        public static Doctor GetDoctorById(VetClinicContext context, int doctorId)
        {
            return context.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);
        }

        public static void UpdateDoctor(VetClinicContext context, int doctorId, Doctor doctor)
        {
            var doctorToUpdate = context.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);
            if (doctorToUpdate != null)
            {
                doctorToUpdate.Name = doctor.Name;
                doctorToUpdate.Specialty = doctor.Specialty;
                context.SaveChanges();
            }
        }

        public static void DeleteDoctor(VetClinicContext context, int doctorId)
        {
            var doctorToDelete = context.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);
            if (doctorToDelete != null)
            {
                context.Doctors.Remove(doctorToDelete);
                context.SaveChanges();
            }
        }
    }
}
