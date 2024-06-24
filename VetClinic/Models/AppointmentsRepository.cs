using System;
using System.Collections.Generic;
using System.Linq;

namespace VetClinic.Models
{
    public static class AppointmentsRepository
    {
        private static List<Appointment> _appointments = new List<Appointment>()
        {
            new Appointment
            {
                AppointmentId = 1,
                StartTime = DateTime.Today.AddHours(9),
                EndTime = DateTime.Today.AddHours(10),
                DoctorId = 1,
                PatientId = 1,
                RoomId = 1
            },
            new Appointment
            {
                AppointmentId = 2,
                StartTime = DateTime.Today.AddDays(1).AddHours(14),
                EndTime = DateTime.Today.AddDays(1).AddHours(15),
                DoctorId = 2,
                PatientId = 2,
                RoomId = 2
            },
            new Appointment
            {
                AppointmentId = 3,
                StartTime = DateTime.Today.AddDays(2).AddHours(11),
                EndTime = DateTime.Today.AddDays(2).AddHours(12),
                DoctorId = 3,
                PatientId = 3,
                RoomId = 3
            },
            new Appointment
            {
                AppointmentId = 4,
                StartTime = DateTime.Today.AddDays(3).AddHours(13),
                EndTime = DateTime.Today.AddDays(3).AddHours(14),
                DoctorId = 4,
                PatientId = 4,
                RoomId = 4
            }
        };

        public static void AddAppointment(Appointment appointment)
        {
            if (_appointments.Any())
            {
                var maxId = _appointments.Max(x => x.AppointmentId);
                appointment.AppointmentId = maxId + 1;
            }
            else
            {
                appointment.AppointmentId = 1;
            }

            LoadRelatedEntities(appointment);

            _appointments.Add(appointment);
        }


        public static List<Appointment> GetAppointments(bool loadRelated = false)
        {
            if (loadRelated)
            {
                _appointments.ForEach(LoadRelatedEntities);
            }
            return _appointments;
        }

        public static Appointment GetAppointmentById(int appointmentId, bool loadRelated = false)
        {
            var appointment = _appointments.FirstOrDefault(x => x.AppointmentId == appointmentId);
            if (appointment != null && loadRelated)
            {
                LoadRelatedEntities(appointment);
            }
            return appointment;
        }

        public static void UpdateAppointment(int appointmentId, Appointment appointment)
        {
            var appointmentToUpdate = _appointments.FirstOrDefault(x => x.AppointmentId == appointmentId);
            if (appointmentToUpdate != null)
            {
                appointmentToUpdate.StartTime = appointment.StartTime;
                appointmentToUpdate.EndTime = appointment.EndTime;
                appointmentToUpdate.DoctorId = appointment.DoctorId;
                appointmentToUpdate.PatientId = appointment.PatientId;
                appointmentToUpdate.RoomId = appointment.RoomId;

                LoadRelatedEntities(appointmentToUpdate);
            }
        }

        public static void DeleteAppointment(int appointmentId)
        {
            var appointment = _appointments.FirstOrDefault(x => x.AppointmentId == appointmentId);
            if (appointment != null)
            {
                _appointments.Remove(appointment);
            }
        }

        private static void LoadRelatedEntities(Appointment appointment)
        {
            appointment.Doctor = DoctorsRepository.GetDoctorById(appointment.DoctorId);
            appointment.Patient = PatientsRepository.GetPatientById(appointment.PatientId);
            appointment.Room = RoomsRepository.GetRoomById(appointment.RoomId);
        }
    }
}
