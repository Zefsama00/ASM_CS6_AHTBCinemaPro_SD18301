﻿using ASM_CS6_AHTBCinemaPro_SD18301.Data;
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
    public class LoaiPhimController : ControllerBase
    {
        private readonly DBCinemaContext _context;

        public LoaiPhimController(DBCinemaContext context)
        {

            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoaiPhim>>> GetLoaiGhes()
        {
            var loaiPhims = await _context.LoaiPhims.ToListAsync();
            return Ok(loaiPhims);
        }

    }
}
