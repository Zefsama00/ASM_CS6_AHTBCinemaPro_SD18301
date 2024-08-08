using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using ASM_CS6_AHTBCinemaPro_SD18301.Shared.Models;
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
    public class DanhMucPhimsController : ControllerBase
    {
        private readonly DBCinemaContext _context;

        public DanhMucPhimsController(DBCinemaContext context)
        {
            _context = context;
        }

        // GET: api/DanhMucPhims/danhmuc
        [HttpGet("danhmuc")]
        public async Task<ActionResult<IEnumerable<DanhMucPhim>>> GetDanhMucPhims()
        {
            var danhMucPhims = await _context.DanhMucPhims
                .Include(g => g.DanhMuc)
                .Include(g => g.Phim)
                .ToListAsync();
            return Ok(danhMucPhims);
        }

        // GET: api/DanhMucPhims/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucPhim>> GetDanhMucPhim(int id)
        {
            var danhMucPhim = await _context.DanhMucPhims
                .Include(g => g.DanhMuc)
                .Include(g => g.Phim)
                .FirstOrDefaultAsync(d => d.IDDanhMucPhim == id);

            if (danhMucPhim == null)
            {
                return NotFound();
            }

            return Ok(danhMucPhim);
        }

        // PUT: api/DanhMucPhims/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDanhMucPhim(int id, DanhMucPhim danhMucPhim)
        {
            if (id != danhMucPhim.IDDanhMucPhim)
            {
                return BadRequest();
            }

            _context.Entry(danhMucPhim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DanhMucPhimExists(id))
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

        // POST: api/DanhMucPhims
        [HttpPost]
        public async Task<ActionResult<DanhMucPhim>> PostDanhMucPhim(DanhMucPhim danhMucPhim)
        {
            _context.DanhMucPhims.Add(danhMucPhim);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDanhMucPhim), new { id = danhMucPhim.IDDanhMucPhim }, danhMucPhim);
        }

        // DELETE: api/DanhMucPhims/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDanhMucPhim(int id)
        {
            var danhMucPhim = await _context.DanhMucPhims.FindAsync(id);
            if (danhMucPhim == null)
            {
                return NotFound();
            }

            _context.DanhMucPhims.Remove(danhMucPhim);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DanhMucPhimExists(int id)
        {
            return _context.DanhMucPhims.Any(e => e.IDDanhMucPhim == id);
        }
    }
}
