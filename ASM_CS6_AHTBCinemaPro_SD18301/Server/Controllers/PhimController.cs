using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using ASM_CS6_AHTBCinemaPro_SD18301.Model;
using ASM_CS6_AHTBCinemaPro_SD18301.Shared.Models;

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

        // GET: api/Phim/chuachieu
        [HttpGet("chuachieu")]
        public async Task<ActionResult<List<Phim>>> GetPhimChuaChieu()
        {
            var phimChuaChieu = await _context.Phims
                .Include(p => p.CaChieus)
                .ToListAsync();

            return Ok(phimChuaChieu);
        }
        [HttpGet]
        [Route("GetRooms")]
        public async Task<ActionResult<IEnumerable<int>>> GetsoPhong()
        {
            var rooms = await _context.Phongs.Select(p => p.SoPhong).ToListAsync();
            return Ok(rooms);
        }
        [HttpGet("danhmuc")]
        public async Task<ActionResult<IEnumerable<string>>> Getdanhmuc()
        {
            var danhmuc = await _context.DanhMucs.ToListAsync();
            return Ok(danhmuc);
        }
        [HttpGet("tenphim")]
        public async Task<ActionResult<IEnumerable<string>>> GetTenPhim()
        {
            var dsphim = await _context.Phims.Select(p => p.TenPhim).ToListAsync();
            return Ok(dsphim);
        }
        [HttpGet("phim")]
        public async Task<ActionResult<IEnumerable<Phim>>> GetPhims()
        {
            var dsphim = await _context.Phims.Include(g => g.DanhMucPhim)
                .ThenInclude(dm => dm.DanhMuc)
                .ToListAsync();
            return Ok(dsphim);
        }

        // GET: api/Phim/listphim
        [HttpGet]
        [Route("listphim")]
        public async Task<ActionResult<List<PhimVM>>> GetPhim()
        {
            try
            {
                var dsphim = await _context.Phims
                    .Include(p => p.LoaiPhim) 
                    .Select(p => new PhimVM
                    {
                        IdPhim = p.IdPhim,
                        TenPhim = p.TenPhim,
                        DienVien = p.DienVien,
                        DangPhim = p.DangPhim,
                        TheLoai = p.LoaiPhim.TenLoai,
                        ThoiLuong = p.ThoiLuong,
                        HinhAnh = p.HinhAnh,
                    })
                    .ToListAsync();

                return Ok(dsphim);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi máy chủ nội bộ: " + ex.Message);
            }
        }



        // GET: api/Phim/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Phim>> GetPhimById(string id)
        {
            var phim = await _context.Phims
                .Include(p => p.CaChieus)
                .FirstOrDefaultAsync(p => p.IdPhim == id);

            if (phim == null)
            {
                return NotFound();
            }

            return Ok(phim);
        }

        // POST: api/Phim
        [HttpPost]
        [Route("AddPhim")]
        public async Task<ActionResult<Phim>> AddPhim(PhimVM phimvm)
        {
            if (string.IsNullOrEmpty(phimvm.IdPhim))
            {
                phimvm.IdPhim = new PhimIdGenerator().Next(null);
            }

            if (_context.Phims.Any(e => e.IdPhim == phimvm.IdPhim))
            {
                return BadRequest("ID Phim đã tồn tại.");
            }
            var phim = new Phim
            {
                IdPhim = phimvm.IdPhim,
                TenPhim = phimvm.TenPhim,
                ThoiLuong = phimvm.ThoiLuong,
                TheLoai = phimvm.TheLoai,
                DienVien = phimvm.DienVien,
                HinhAnh = phimvm.HinhAnh,
                DangPhim = phimvm.DangPhim,
            };
            //var theloai = await _context.LoaiPhims.FirstOrDefaultAsync(x => x.TenLoai == phim.TheLoai);

            //if (theloai == null)
            //{
            //    return BadRequest("Thể loại không tồn tại.");
            //}

            //phim.TheLoai = theloai.IdLP;


            _context.Phims.Add(phim);
            await _context.SaveChangesAsync();


            var danhMucPhim = new DanhMucPhim
            {
                IdDanhMuc = 1,
                IdPhim = phimvm.IdPhim
            };

            _context.DanhMucPhims.Add(danhMucPhim);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPhim), new { id = phim.IdPhim }, phim);
        }



        // PUT: api/Phim/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePhim(string id, [FromBody] PhimVM updatedPhimVM)
        {
            // Check if the record with the provided ID exists
            //if (!await _context.Phims.AnyAsync(e => e.IdPhim == id))
            //{
            //    return BadRequest("ID không tồn tại.");
            //}

            //// Find the movie genre (LoaiPhim) by name
            //var theloai = await _context.LoaiPhims.FirstOrDefaultAsync(x => x.TenLoai == updatedPhimVM.TheLoai);
            //if (theloai == null)
            //{
            //    return BadRequest("Thể loại không tồn tại.");
            //}

            // Retrieve the existing movie (Phim) entity
            var existingPhim = await _context.Phims.FindAsync(id);
            if (existingPhim == null)
            {
                return NotFound();
            }

            // Update the existing Phim entity with the new values
            existingPhim.TenPhim = updatedPhimVM.TenPhim;
            existingPhim.DienVien = updatedPhimVM.DienVien;
            existingPhim.DangPhim = updatedPhimVM.DangPhim;
            existingPhim.TheLoai = updatedPhimVM.TheLoai; // Update to the correct LoaiPhim ID
            existingPhim.ThoiLuong = updatedPhimVM.ThoiLuong;
            existingPhim.HinhAnh = updatedPhimVM.HinhAnh;

            // Mark the entity as modified
            _context.Entry(existingPhim).State = EntityState.Modified;

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhimExists(id))
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

        // DELETE: api/Phim/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePhim(string id)
        {
            var phim = await _context.Phims.FindAsync(id);
            if (phim == null)
            {
                return NotFound();
            }

            _context.Phims.Remove(phim);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhimExists(string id)
        {
            return _context.Phims.Any(p => p.IdPhim == id);
        }

        // Generator ID cho Phim
        public class PhimIdGenerator : ValueGenerator<string>
        {
            public override bool GeneratesTemporaryValues => false;

            public override string Next(EntityEntry entry)
            {
                return "P" + Guid.NewGuid().ToString().Substring(0, 2).ToUpper();
            }
        }
    }
}