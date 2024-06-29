using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VetClinic.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "DoctorId", "Name", "Specialty" },
                values: new object[,]
                {
                    { 1, "Dr. Gregory House", "Diagnostic Medicine" },
                    { 2, "Dr. Meredith Grey", "General Surgery" },
                    { 3, "Dr. John Watson", "General Practice" },
                    { 4, "Dr. Stephen Strange", "Neurosurgery" },
                    { 5, "Dr. Miranda Bailey", "General Surgery" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "Description", "IsOccupied", "RoomNumber", "Type" },
                values: new object[,]
                {
                    { 1, "First floor examination room", false, "A101", "Examination Room" },
                    { 2, "Second floor operating theater", true, "B205", "Operating Theater" },
                    { 3, "Third floor examination room", false, "C302", "Examination Room" },
                    { 4, "Fourth floor hospitalization room", true, "D104", "Hospitalization Room" },
                    { 5, "Second floor examination room", false, "E201", "Examination Room" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "Age", "Breed", "Diagnosis", "DoctorId", "OwnerInfo", "PatientName", "Type" },
                values: new object[,]
                {
                    { 1, 3, "Labrador", "Healthy, routine check-up", 5, "John Doe", "Bella", "Dog" },
                    { 2, 5, "German Shepherd", "Skin allergy, medication prescribed", 1, "Jane Smith", "Max", "Dog" },
                    { 3, 2, "Poodle", "Fractured leg, recovering well", 4, "Emily Johnson", "Lucy", "Dog" },
                    { 4, 4, "Bulldog", "Obesity, diet plan recommended", 2, "Michael Brown", "Charlie", "Dog" },
                    { 5, 1, "Syrian Hamster", "Respiratory infection, antibiotics prescribed", 3, "Alice White", "Nibbles", "Hamster" },
                    { 6, 2, "Canary", "Feather plucking, behavioral management", 4, "Bob Green", "Tweety", "Bird" },
                    { 7, 3, "Sports Car", "Engine overhaul required", 1, "Chris Blue", "Speedster", "Other" },
                    { 8, 6, "Thoroughbred", "Hoof injury, bandage and rest advised", 5, "Diana Black", "Thunder", "Horse" },
                    { 9, 2, "Dwarf Hamster", "Eye infection, eye drops prescribed", 3, "Eve Brown", "Whiskers", "Hamster" },
                    { 10, 4, "Parrot", "Beak overgrowth, trim performed", 4, "Frank Yellow", "Polly", "Bird" },
                    { 11, 10, "Volkswagen Beetle", "Transmission issue, parts replacement needed", 1, "George Purple", "Herbie", "Other" },
                    { 12, 8, "Arabian Horse", "Colic episode, monitored closely", 5, "Helen Silver", "Majesty", "Horse" },
                    { 13, 1, "Roborovski Hamster", "Dehydration, rehydration therapy", 3, "Ian Gold", "Squeaky", "Hamster" },
                    { 14, 3, "Cockatiel", "Wing injury, splint applied", 4, "Jack White", "Sky", "Bird" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "DoctorId", "EndTime", "PatientId", "RoomId", "StartTime" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 6, 29, 10, 0, 0, 0, DateTimeKind.Local), 1, 1, new DateTime(2024, 6, 29, 9, 0, 0, 0, DateTimeKind.Local) },
                    { 2, 2, new DateTime(2024, 6, 30, 15, 0, 0, 0, DateTimeKind.Local), 2, 2, new DateTime(2024, 6, 30, 14, 0, 0, 0, DateTimeKind.Local) },
                    { 3, 3, new DateTime(2024, 7, 1, 12, 0, 0, 0, DateTimeKind.Local), 3, 3, new DateTime(2024, 7, 1, 11, 0, 0, 0, DateTimeKind.Local) },
                    { 4, 4, new DateTime(2024, 7, 2, 14, 0, 0, 0, DateTimeKind.Local), 4, 4, new DateTime(2024, 7, 2, 13, 0, 0, 0, DateTimeKind.Local) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 5);
        }
    }
}
