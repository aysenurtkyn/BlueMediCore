namespace BlueMediCore.DTOs
{
    public class PatientDtos
    {
        public class PatientResponseDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string IdentityNumber { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
        }

        public class CreatePatientDto
        {
            public string Name { get; set; } = string.Empty;
            public string IdentityNumber { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
        }
    }
}