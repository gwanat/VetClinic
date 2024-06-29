using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VetClinic.Models
{
    public static class AppointmentsRepository
    {
        public static void AddAppointment(Appointment appointment, VetClinicContext context)
        {
            context.Appointments.Add(appointment);
            context.SaveChanges();
            UpdateRoomOccupationStatus(appointment.RoomId, context);
        }

        public static List<Appointment> GetAppointments(bool loadRelated = false, VetClinicContext context = null)
        {
            var appointments = context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).Include(a => a.Room).ToList();

            if (loadRelated)
            {
                appointments.ForEach(a => UpdateRoomOccupationStatus(a.RoomId, context));
            }
            return appointments;
        }

        public static Appointment GetAppointmentById(int appointmentId, bool loadRelated = false, VetClinicContext context = null)
        {
            var appointment = context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.Room)
                .FirstOrDefault(a => a.AppointmentId == appointmentId);

            if (appointment != null && loadRelated)
            {
                UpdateRoomOccupationStatus(appointment.RoomId, context);
            }
            return appointment;
        }

        public static void UpdateAppointment(int appointmentId, Appointment appointment, VetClinicContext context)
        {
            var appointmentToUpdate = context.Appointments.Find(appointmentId);
            if (appointmentToUpdate != null)
            {
                appointmentToUpdate.StartTime = appointment.StartTime;
                appointmentToUpdate.EndTime = appointment.EndTime;
                appointmentToUpdate.DoctorId = appointment.DoctorId;
                appointmentToUpdate.PatientId = appointment.PatientId;
                appointmentToUpdate.RoomId = appointment.RoomId;

                context.SaveChanges();
                UpdateRoomOccupationStatus(appointmentToUpdate.RoomId, context);
            }
        }

        public static void DeleteAppointment(int appointmentId, VetClinicContext context)
        {
            var appointment = context.Appointments.Find(appointmentId);
            if (appointment != null)
            {
                context.Appointments.Remove(appointment);
                context.SaveChanges();
                UpdateRoomOccupationStatus(appointment.RoomId, context);
            }
        }

        private static void UpdateRoomOccupationStatus(int roomId, VetClinicContext context)
        {
            var room = context.Rooms
                .Include(r => r.Appointments)
                .FirstOrDefault(r => r.RoomId == roomId);
            if (room != null)
            {
                room.UpdateOccupationStatus();
                context.SaveChanges();
            }
        }
    }
}
