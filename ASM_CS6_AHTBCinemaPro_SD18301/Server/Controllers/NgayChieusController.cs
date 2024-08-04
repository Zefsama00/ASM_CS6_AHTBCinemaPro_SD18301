using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NgayChieusController : ControllerBase
    {
        private readonly DBCinemaContext _context;

        public NgayChieusController(DBCinemaContext context)
        {
            _context = context;
        }

        // GET: api/NgayChieus/GetNgayChieus
        [HttpGet]
        [Route("GetNgayChieus")]
        public async Task<ActionResult<IEnumerable<NgayChieu>>> GetNgayChieus()
        {
            var ngayChieus = await _context.NgayChieus.ToListAsync();

            if (ngayChieus == null || !ngayChieus.Any())
                {
                return NotFound(new { Message = "No show dates found." });
            }

            return Ok(ngayChieus);
        }
    }
}
