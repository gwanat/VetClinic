using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VetClinic.Models
{
    public static class AppointmentsRepository
    {
        public static void AddAppointment(Appointment appointment, VetClinicContext context)
        {
            try
            {
                context.Appointments.Add(appointment);
                context.SaveChanges();
                UpdateRoomOccupationStatus(appointment.RoomId, context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddAppointment: {ex.Message}");
                throw;
            }
        }

        public static List<Appointment> GetAppointments(bool loadRelated = false, VetClinicContext context = null)
        {
            try
            {
                var appointments = context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).Include(a => a.Room).ToList();

                if (loadRelated)
                {
                    appointments.ForEach(a => UpdateRoomOccupationStatus(a.RoomId, context));
                }
                return appointments;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAppointments: {ex.Message}");
                throw;
            }
        }

        public static Appointment GetAppointmentById(int appointmentId, bool loadRelated = false, VetClinicContext context = null)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAppointmentById: {ex.Message}");
                throw;
            }
        }

        public static void UpdateAppointment(int appointmentId, Appointment appointment, VetClinicContext context)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAppointment: {ex.Message}");
                throw;
            }
        }

        public static void DeleteAppointment(int appointmentId, VetClinicContext context)
        {
            try
            {
                var appointment = context.Appointments.Find(appointmentId);
                if (appointment != null)
                {
                    context.Appointments.Remove(appointment);
                    context.SaveChanges();
                    UpdateRoomOccupationStatus(appointment.RoomId, context);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteAppointment: {ex.Message}");
                throw;
            }
        }

        private static void UpdateRoomOccupationStatus(int roomId, VetClinicContext context)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateRoomOccupationStatus: {ex.Message}");
                throw;
            }
        }
    }
}
