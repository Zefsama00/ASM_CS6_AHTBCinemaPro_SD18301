using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiPhimController : ControllerBase
    {
        private readonly DBCinemaContext _context;

        public LoaiPhimController(DBCinemaContext context)
        {

            _context = context;
        }

        [HttpGet]
        [Route("GetLoaiPhim")]
        public ActionResult<List<string>> GetLoai()
        {
            var loaiPhims = _context.LoaiPhims.Select(x=> x.TenLoai).ToList();
            return Ok(loaiPhims);
        }

    }
}
