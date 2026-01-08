using BlueMediCore.Data;
using BlueMediCore.DTOs;
using BlueMediCore.Entities;
using Microsoft.EntityFrameworkCore;
using static BlueMediCore.DTOs.PatientDtos;

namespace BlueMediCore.Services
{
    public class PatientService
    {
        private readonly AppDbContext _context;

        public PatientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<PatientResponseDto>>> GetAllPatientsAsync()
        {
            var patients = await _context.Patients
                .Select(p => new PatientResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    IdentityNumber = p.IdentityNumber,
                    PhoneNumber = p.PhoneNumber
                }).ToListAsync();

            return ServiceResponse<List<PatientResponseDto>>.Ok(patients, "Hastalar başarıyla listelendi.");
        }

        public async Task<ServiceResponse<PatientResponseDto>> AddPatientAsync(CreatePatientDto dto)
        {
            var patient = new Patient
            {
                Name = dto.Name,
                IdentityNumber = dto.IdentityNumber,
                PhoneNumber = dto.PhoneNumber
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            var response = new PatientResponseDto { Id = patient.Id, Name = patient.Name };
            return ServiceResponse<PatientResponseDto>.Ok(response, "Hasta başarıyla kaydedildi.");
        }
    }
}