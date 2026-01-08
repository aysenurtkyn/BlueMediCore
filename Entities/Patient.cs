namespace BlueMediCore.Entities
{
    public class Patient : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string IdentityNumber { get; set; } = string.Empty; // Servis buradaki ismi arıyor
        public string PhoneNumber { get; set; } = string.Empty;
    }
}