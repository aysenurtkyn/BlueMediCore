namespace BlueMediCore.Entities
{
    public class Appointment : BaseEntity
    {
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = "Pending";

        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; } = null!;

        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; } = null!;
    }
}