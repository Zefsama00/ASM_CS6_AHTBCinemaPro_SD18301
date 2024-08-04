using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiGheController : ControllerBase
    {
        private readonly DBCinemaContext _context;

        public LoaiGheController(DBCinemaContext context)
        {

            _context = context;
        }

        // Lấy danh sách tất cả ghế
        [HttpGet]
        [Route("GetSeatTypes")]
        public ActionResult<List<string>> GetSeatTypes()
        {
            var seatTypes = _context.LoaiGhes.Select(l => l.TenLoaiGhe).ToList(); // Assuming LoaiGhe has a property named TenLoaiGhe
            return Ok(seatTypes);
        }

    }
}
