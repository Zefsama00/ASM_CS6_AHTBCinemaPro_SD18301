using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ASM_CS6_AHTBCinemaPro_SD18301.Server.Controllers.PhimController;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhongController : ControllerBase
    {
        private readonly DBCinemaContext _context;

        public PhongController(DBCinemaContext context)
        {
            _context = context;
        }

        // GET: api/phong
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhongVM>>> GetPhongs()
        {
            var dsphong = await _context.Phongs.Select(
                x => new PhongVM
                {
                    IdPhong = x.IdPhong,
                    SoPhong = x.SoPhong,
                    SoLuongGhe = x.SoLuongGhe,
                    TrangThai = x.TrangThai
                })
                .ToListAsync();
            return Ok(dsphong);
        }

        // GET: api/phong/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Phong>> GetPhong(string id)
        {
            var phong = await _context.Phongs.FindAsync(id);

            if (phong == null)
            {
                return NotFound();
            }

            return phong;
        }
        // POST: api/phong
        [HttpPost]
        public async Task<ActionResult<Phong>> PostPhong(Phong phong)
        {

            if (string.IsNullOrEmpty(phong.IdPhong))
            {
                phong.IdPhong = new PhongGenerator().Next(null);
            }

            _context.Phongs.Add(phong);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPhong), new { id = phong.IdPhong }, phong);
        }

        // PUT: api/phong/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhong(string id, Phong phong)
        {
            if (id != phong.IdPhong)
            {
                return BadRequest("ID phòng không khớp.");
            }

            var existingPhong = await _context.Phongs.FindAsync(id);
            if (existingPhong == null)
            {
                return NotFound();
            }

            existingPhong.SoPhong = phong.SoPhong;
            existingPhong.SoLuongGhe = phong.SoLuongGhe;  // Sửa lỗi này
            existingPhong.TrangThai = phong.TrangThai;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhongExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        // DELETE: api/phong/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhong(string id)
        {
            var phong = await _context.Phongs.FindAsync(id);
            if (phong == null)
            {
                return NotFound();
            }

            _context.Phongs.Remove(phong);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhongExists(string id)
        {
            return _context.Phongs.Any(e => e.IdPhong == id);
        }
        public class PhongGenerator : ValueGenerator<string>
        {
            public override bool GeneratesTemporaryValues => false;

            public override string Next(EntityEntry entry)
            {
                Random random = new Random();
                int randomNumber1 = random.Next(0, 10);
                int randomNumber2 = random.Next(0, 10);
                return $"PG{randomNumber1}{randomNumber2}";
            }

        }
    }
}