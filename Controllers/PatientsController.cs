using BlueMediCore.Services;
using Microsoft.AspNetCore.Mvc;
using static BlueMediCore.DTOs.PatientDtos;

namespace BlueMediCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientService _patientService;

        public PatientsController(PatientService patientService)
        {
            _patientService = patientService;
        }

        // GET: api/patients
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _patientService.GetAllPatientsAsync();
            return Ok(result);
        }

        // POST: api/patients
        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientDto dto)
        {
            var result = await _patientService.AddPatientAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
