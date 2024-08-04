using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public async Task<IActionResult> GetNgayChieus()
        {
            var ngayChieus = await _context.NgayChieus
                .Select(g => new NgayChieuVM
                {
                    IdCaChieu = g.IdCaChieu,
                    Phong = g.Phongs.SoPhong,
                    Phim = g.Phims.TenPhim,
                    NgayChieu = g.NgayChieuPhim,
                    TrangThai = g.TrangThai
                })
                .ToListAsync();

            return Ok(ngayChieus);
        }
    }
}
