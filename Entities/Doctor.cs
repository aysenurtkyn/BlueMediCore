namespace BlueMediCore.Entities
{
    public class Doctor:BaseEntity
    {
        public required string Name { get; set; }
        public required string Branch { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
