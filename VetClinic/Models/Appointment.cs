using VetClinic.Models;

public class Appointment
{
    public int AppointmentId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string ClientPhoneNumber { get; set; } = string.Empty;
    public int DoctorId { get; set; } 
    public Doctor? Doctor { get; set; } 
    public int PatientId { get; set; } 
    public Patient? Patient { get; set; } 
    public int RoomId { get; set; } 
    public Room? Room { get; set; } 
}
