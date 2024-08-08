using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using ASM_CS6_AHTBCinemaPro_SD18301.Shared.ViewModels;
using System.Net.NetworkInformation;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GhesController : ControllerBase
    {
        private readonly DBCinemaContext _context;

        public GhesController(DBCinemaContext context)
        {
            _context = context;
        }

        // GET: api/Ghes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GheVM>>> GetGhes()
        {
            return await _context.Ghes
                .Include(g => g.Phongs)
                .Include(g => g.LoaiGhes).Select(x => new GheVM
                {
                    IdGhe = x.IdGhe,
                    TenGhe = x.TenGhe,
                    Phong = x.Phongs.SoPhong.ToString(),
                    TrangThai = x.TrangThai,
                    LoaiGhe = x.LoaiGhes.TenLoaiGhe,
                })
                .ToListAsync();
        }

        // POST: api/Ghes/BulkCreate
        [HttpPost]
        public async Task<ActionResult> BulkCreateGhes(BulkCreateGheViewModel bulkCreateGheViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newGhes = new List<Ghe>();
            var newVes = new List<Ve>();

            string seatLetter = bulkCreateGheViewModel.StartingSeatLetter;

            int countGhes = await _context.Ghes.CountAsync();

            int gheIndex = countGhes + 1;

            for (int i = 0; i < bulkCreateGheViewModel.SoLuongGhe; i++)
            {

                string idGhe = "GE" + gheIndex;
                var ghe = new Ghe
                {
                    IdGhe = idGhe,
                    TenGhe = $"{seatLetter}{i + 1}",
                    Phong = bulkCreateGheViewModel.Phong,
                    TrangThai = bulkCreateGheViewModel.TrangThai,
                    LoaiGhe = bulkCreateGheViewModel.LoaiGhe
                };
                newGhes.Add(ghe);
                float giaVe = bulkCreateGheViewModel.GiaVe;

                gheIndex++;
                var ve = new Ve
                {
                    TenVe = $"Vé - {ghe.TenGhe}",
                    GiaVe = giaVe,
                    SuatChieu = bulkCreateGheViewModel.GioChieuId.Value, // Save GioChieuId to Ve
                    Ghe = ghe.IdGhe,
                };
                newVes.Add(ve);
            }
            await _context.Ghes.AddRangeAsync(newGhes);
            await _context.SaveChangesAsync();
            await _context.Ves.AddRangeAsync(newVes);
            await _context.SaveChangesAsync();

            return Ok(new { Ghes = newGhes, Ves = newVes });
        }
        // PUT: api/Ghes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGhe(string id, GheVM gheVM)
        {
            if (id != gheVM.IdGhe)
            {
                return BadRequest();
            }

            var ghe = await _context.Ghes.FindAsync(id);
            if (ghe == null)
            {
                return NotFound();
            }
            ghe.TenGhe = gheVM.TenGhe;
            ghe.TrangThai = gheVM.TrangThai;
            var existingLoaiGhe = _context.LoaiGhes.FirstOrDefault(x => x.IdLoaiGhe == gheVM.LoaiGhe);
            if (existingLoaiGhe != null)
            {
                ghe.LoaiGhe = gheVM.LoaiGhe;
            }


            _context.Entry(ghe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GheExists(id))
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

        private bool GheExists(string id)
        {
            return _context.Ghes.Any(e => e.IdGhe == id);
        }
    }
}