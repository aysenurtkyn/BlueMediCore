using BlueMediCore.Services;
using Microsoft.AspNetCore.Mvc;
using static BlueMediCore.DTOs.AppointmentDtos;

namespace BlueMediCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;

        public AppointmentsController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentDto dto)
        {
            var result = await _appointmentService.AddAppointmentAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAppointmentDto dto)
        {
            var result = await _appointmentService.UpdateAppointmentAsync(id, dto);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }


    }
}
