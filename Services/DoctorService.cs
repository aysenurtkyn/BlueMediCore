using BlueMediCore.Data;
using BlueMediCore.DTOs;
using BlueMediCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BlueMediCore.DTOs.DoctorDtos;

namespace BlueMediCore.Services
{
    public class DoctorService
    {
        private readonly AppDbContext _context;

        public DoctorService(AppDbContext context)
        {
            _context = context;
        }

        // Tüm doktorları getir
        public async Task<ServiceResponse<List<DoctorResponseDto>>> GetAllDoctorsAsync()
        {
            var doctors = await _context.Doctors
                .Where(d => !d.IsDeleted)
                .Select(d => new DoctorResponseDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Branch = d.Branch
                })
                .ToListAsync();

            // Yeni yapıyı kullanıyoruz
            return ServiceResponse<List<DoctorResponseDto>>.Ok(doctors, "Doktorlar listelendi.");
        }

        // Yeni doktor ekle
        public async Task<ServiceResponse<DoctorResponseDto>> AddDoctorAsync(CreateDoctorDto dto)
        {
            // Basit bir doğrulama örneği
            if (string.IsNullOrEmpty(dto.Name))
            {
                return ServiceResponse<DoctorResponseDto>.Fail("Doktor adı boş olamaz!");
            }

            var newDoctor = new Doctor
            {
                Name = dto.Name,
                Branch = dto.Branch
            };

            _context.Doctors.Add(newDoctor);
            await _context.SaveChangesAsync();

            var responseDto = new DoctorResponseDto
            {
                Id = newDoctor.Id,
                Name = newDoctor.Name,
                Branch = newDoctor.Branch
            };

            return ServiceResponse<DoctorResponseDto>.Ok(responseDto, "Doktor başarıyla eklendi.");
        }
    }
}