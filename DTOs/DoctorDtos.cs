namespace BlueMediCore.DTOs
{
    public class DoctorDtos
    {
        public class DoctorResponseDto
        {
            public int Id { get; set; }
            public required string Name { get; set; }
            public required string Branch { get; set; }
        }

        public class CreateDoctorDto
        {
            public required string Name { get; set; }
            public required string Branch { get; set; }
        }
        public class UpdateDoctorDto
        {
            public required string Name { get; set; }
            public required string Branch { get; set; }
        }
    }
}
