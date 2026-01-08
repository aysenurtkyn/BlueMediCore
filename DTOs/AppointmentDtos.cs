namespace BlueMediCore.DTOs
{
    public class AppointmentDtos
    {
        public class AppointmentResponseDto
        {
            public int Id { get; set; }
            public DateTime AppointmentDate { get; set; }
            public string Status { get; set; }

            // İlişkili verilerin isimlerini de göstermek kullanıcı dostudur
            public int DoctorId { get; set; }
            public string DoctorName { get; set; }
            public int PatientId { get; set; }
            public string PatientName { get; set; }
        }

        public class CreateAppointmentDto
        {
            public DateTime AppointmentDate { get; set; }
            public int DoctorId { get; set; }
            public int PatientId { get; set; }
        }

        public class UpdateAppointmentDto
        {
            public DateTime? AppointmentDate { get; set; }
            public string? Status { get; set; }
        }
    }
}