using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using ASM_CS6_AHTBCinemaPro_SD18301.Client.Pages;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhuyenmaisController : ControllerBase
    {
        private readonly DBCinemaContext _context;

        public KhuyenmaisController(DBCinemaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetKhuyenmais")]
        public async Task<ActionResult<IEnumerable<KhuyenMai>>> GetKhuyenmais()
        {
            try
            {
                var khuyenmais = await _context.KhuyenMais.ToListAsync();
                if (khuyenmais == null || !khuyenmais.Any())
                {
                    return NotFound(new { Message = "No promotions found." });
                }
                return Ok(khuyenmais);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while retrieving promotions.", Details = ex.Message });
            }
        }

        // Add other methods (e.g., POST, PUT, DELETE) if necessary
    }
}
