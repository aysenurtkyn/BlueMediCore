using BlueMediCore.DTOs;
using BlueMediCore.Services;
using Microsoft.AspNetCore.Mvc;
using static BlueMediCore.DTOs.DoctorDtos;

namespace BlueMediCore.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
  
    public class DoctorsController : ControllerBase
    {
       
        private readonly DoctorService _doctorService;

        
        public DoctorsController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            var result = await _doctorService.GetAllDoctorsAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDoctorDto dto)
        {
            var result = await _doctorService.AddDoctorAsync(dto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}