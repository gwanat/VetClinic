using CoreBusiness;
using Microsoft.EntityFrameworkCore;

namespace Plugins.DataStore.SQL
{
    public class VetClinicContext :DbContext
    {
        public VetClinicContext(DbContextOptions options): base(options)
        {
                
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Patients)
                .WithOne(p => p.Doctor)
                .HasForeignKey(p => p.DoctorId);

            // Doctor - Appointment: One-to-Many
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Appointments)
                .WithOne(a => a.Doctor)
                .HasForeignKey(a => a.DoctorId);

            // Patient - Appointment: One-to-Many
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId);

            // Room - Appointment: One-to-Many
            modelBuilder.Entity<Room>()
                .HasMany(r => r.Appointments)
                .WithOne(a => a.Room)
                .HasForeignKey(a => a.RoomId);

            //seeding data

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { DoctorId = 1, Name = "Dr. Gregory House", Specialty = "Diagnostic Medicine" },
                new Doctor { DoctorId = 2, Name = "Dr. Meredith Grey", Specialty = "General Surgery" },
                new Doctor { DoctorId = 3, Name = "Dr. John Watson", Specialty = "General Practice" },
                new Doctor { DoctorId = 4, Name = "Dr. Stephen Strange", Specialty = "Neurosurgery" },
                new Doctor { DoctorId = 5, Name = "Dr. Miranda Bailey", Specialty = "General Surgery" }
                );

            modelBuilder.Entity<Appointment>().HasData(
                new Patient { PatientId = 1, DoctorId = 1, PatientName = "Bella", Type = "Dog", Breed = "Labrador", Age = 3, OwnerInfo = "John Doe", Diagnosis = "Healthy, routine check-up" },
                new Patient { PatientId = 2, DoctorId = 1, PatientName = "Max", Type = "Dog", Breed = "German Shepherd", Age = 5, OwnerInfo = "Jane Smith", Diagnosis = "Skin allergy, medication prescribed" },
                new Patient { PatientId = 3, DoctorId = 3, PatientName = "Lucy", Type = "Dog", Breed = "Poodle", Age = 2, OwnerInfo = "Emily Johnson", Diagnosis = "Fractured leg, recovering well" },
                new Patient { PatientId = 4, DoctorId = 4, PatientName = "Charlie", Type = "Dog", Breed = "Bulldog", Age = 4, OwnerInfo = "Michael Brown", Diagnosis = "Obesity, diet plan recommended" },
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
                );
            modelBuilder.Entity<Room>().HasData(
                new Room { RoomId = 1, RoomNumber = "A101", Type = "Examination Room", IsOccupied = false, Description = "First floor examination room" },
                new Room { RoomId = 2, RoomNumber = "B205", Type = "Operating Theater", IsOccupied = true, Description = "Second floor operating theater" },
                new Room { RoomId = 3, RoomNumber = "C302", Type = "Examination Room", IsOccupied = false, Description = "Third floor examination room" },
                new Room { RoomId = 4, RoomNumber = "D104", Type = "Hospitalization Room", IsOccupied = true, Description = "Fourth floor hospitalization room" },
                new Room { RoomId = 5, RoomNumber = "E201", Type = "Examination Room", IsOccupied = false, Description = "Second floor examination room" }
                );

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment { AppointmentId = 1, StartTime = DateTime.Today.AddHours(9), EndTime = DateTime.Today.AddHours(10), DoctorId = 1, PatientId = 1, RoomId = 1 },
                new Appointment { AppointmentId = 2, StartTime = DateTime.Today.AddDays(1).AddHours(14), EndTime = DateTime.Today.AddDays(1).AddHours(15), DoctorId = 2, PatientId = 2, RoomId = 2 },
                new Appointment { AppointmentId = 3, StartTime = DateTime.Today.AddDays(2).AddHours(11), EndTime = DateTime.Today.AddDays(2).AddHours(12), DoctorId = 3, PatientId = 3, RoomId = 3 },
                new Appointment { AppointmentId = 4, StartTime = DateTime.Today.AddDays(3).AddHours(13), EndTime = DateTime.Today.AddDays(3).AddHours(14), DoctorId = 4, PatientId = 4, RoomId = 4 }
            );
        }
    }
}
