using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly DBCinemaContext _context;

        public KhachHangController(DBCinemaContext context)
        {
            _context = context;
        }

        // GET: api/KhachHang/{idUser}
        [HttpGet("{idUser}")]
        public async Task<IActionResult> GetKhachHangByIdUser(string idUser)
        {
            if (string.IsNullOrEmpty(idUser))
            {
                return BadRequest("IdUser cannot be null or empty.");
            }

            // Query the KhachHang table to find the record by IdUser
            var khachHang = await _context.KhachHangs
                .Include(kh => kh.Users)  // Include any related entities if needed
                .FirstOrDefaultAsync(kh => kh.IDUser == idUser);

            if (khachHang == null)
            {
                return NotFound($"No customer found with IdUser: {idUser}");
            }

            return Ok(khachHang);
        }
    }
}
