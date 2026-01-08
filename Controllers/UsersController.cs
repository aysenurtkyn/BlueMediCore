using Microsoft.AspNetCore.Mvc;

namespace BlueMediCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // ŞİMDİLİK BOŞ
        // UserService eklendiğinde dolduracağız

        [HttpGet]
        public IActionResult Info()
        {
            return Ok("User controller aktif.");
        }
    }
}
