using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace VetClinic.Models
{
    public class VetClinicContext : DbContext
    {
        public VetClinicContext(DbContextOptions<VetClinicContext> options) : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Doctor>().HasKey(d => d.DoctorId);
            modelBuilder.Entity<Patient>().HasKey(p => p.PatientId);
            modelBuilder.Entity<Room>().HasKey(r => r.RoomId);
            modelBuilder.Entity<Appointment>().HasKey(a => a.AppointmentId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Room)
                .WithMany(r => r.Appointments)
                .HasForeignKey(a => a.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
