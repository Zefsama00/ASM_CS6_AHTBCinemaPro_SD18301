using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using Microsoft.EntityFrameworkCore;
namespace ASM_CS6_AHTBCinemaPro_SD18301.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhimController : ControllerBase
    {
        private readonly DBCinemaContext _context;

        public PhimController(DBCinemaContext context)
        {
            _context = context;
        }

        // GET: api/PhimChuaChieu
        [HttpGet]
        [Route("chuachieu")]
        public ActionResult<List<Phim>> GetPhimChuaChieu()
        {
            var phimChuaChieu = _context.Phims
        .Include(p => p.CaChieus)
        .Where(p => p.CaChieus.Any(nc => nc.TrangThai == "Chưa Chiếu"))
        .ToList();

            return Ok(phimChuaChieu);
        }
    }
}
