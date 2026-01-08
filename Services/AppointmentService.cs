using BlueMediCore.Data;
using BlueMediCore.DTOs;
using BlueMediCore.Entities;
using Microsoft.EntityFrameworkCore;
using static BlueMediCore.DTOs.AppointmentDtos;

namespace BlueMediCore.Services
{
    public class AppointmentService
    {
        private readonly AppDbContext _context;

        public AppointmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<AppointmentResponseDto>>> GetAllAppointmentsAsync()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Doctor) // Doktor verisini çek
                .Include(a => a.Patient) // Hasta verisini çek
                .Select(a => new AppointmentResponseDto
                {
                    Id = a.Id,
                    AppointmentDate = a.AppointmentDate,
                    Status = a.Status,
                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.Name, // İlişkili veriden isim al
                    PatientId = a.PatientId,
                    PatientName = a.Patient.Name // İlişkili veriden isim al
                }).ToListAsync();

            return ServiceResponse<List<AppointmentResponseDto>>.Ok(appointments, "Randevular başarıyla listelendi.");
        }

        public async Task<ServiceResponse<AppointmentResponseDto>> AddAppointmentAsync(CreateAppointmentDto dto)
        {
            // Doktor var mı kontrol et
            var doctorExists = await _context.Doctors
                .AnyAsync(d => d.Id == dto.DoctorId && !d.IsDeleted);

            if (!doctorExists)
                return ServiceResponse<AppointmentResponseDto>
                    .Fail("Seçilen doktor bulunamadı.");

            //  Hasta var mı kontrol et
            var patientExists = await _context.Patients
                .AnyAsync(p => p.Id == dto.PatientId && !p.IsDeleted);

            if (!patientExists)
                return ServiceResponse<AppointmentResponseDto>
                    .Fail("Seçilen hasta bulunamadı.");

            // Yeni randevu entity oluştur
            var appointment = new Appointment
            {
                AppointmentDate = dto.AppointmentDate,
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                Status = "Pending"
            };

            //  DB’ye ekle
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Response DTO oluştur
            var response = new AppointmentResponseDto
            {
                Id = appointment.Id,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId
            };

            return ServiceResponse<AppointmentResponseDto>
                .Ok(response, "Randevu başarıyla oluşturuldu.");
        }


        public async Task<ServiceResponse<string>> UpdateAppointmentAsync(int appointmentId,UpdateAppointmentDto dto)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == appointmentId && !a.IsDeleted);

            if (appointment == null)
                return ServiceResponse<string>.Fail("Randevu bulunamadı.");

            if (dto.AppointmentDate.HasValue)
                appointment.AppointmentDate = dto.AppointmentDate.Value;

            if (!string.IsNullOrEmpty(dto.Status))
                appointment.Status = dto.Status;

            await _context.SaveChangesAsync();

            return ServiceResponse<string>
                .Ok("Güncellendi", "Randevu başarıyla güncellendi.");
        }


    }
}