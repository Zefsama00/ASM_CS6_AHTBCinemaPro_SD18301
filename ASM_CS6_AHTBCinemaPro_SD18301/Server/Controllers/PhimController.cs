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
                    .Where(p => p.CaChieus.Any(nc => nc.TrangThai == "Chưa Chiếu"))
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

        [HttpGet("tenphim")]
        public async Task<ActionResult<IEnumerable<string>>> GetTenPhim()
        {
            var dsphim = await _context.Phims.Select(p => p.TenPhim).ToListAsync();
            return Ok(dsphim);
        }

        // GET: api/Phim/listphim
        [HttpGet("listphim")]
        public async Task<ActionResult<List<PhimVM>>> GetPhim()
        {
            try
            {
                var dsphim = await _context.Phims
                    .Join(_context.LoaiPhims,
                        phim => phim.TheLoai,
                        loai => loai.IdLP,
                        (phim, loai) => new { phim, loai })
                    .Join(_context.NgayChieus,
                        combined => combined.phim.CaChieus.FirstOrDefault().IdCaChieu,
                        gioChieu => gioChieu.IdCaChieu,
                        (combined, gioChieu) => new PhimVM
                        {
                            IdPhim = combined.phim.IdPhim,
                            TenPhim = combined.phim.TenPhim,
                            DienVien = combined.phim.DienVien,
                            DangPhim = combined.phim.DangPhim,
                            TheLoai = combined.loai.TenLoai,
                            ThoiLuong = combined.phim.ThoiLuong,
                            HinhAnh = combined.phim.HinhAnh,
                            TrangThai = gioChieu.TrangThai
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
            public async Task<ActionResult<Phim>> AddPhim(Phim phim)
            {
                if (string.IsNullOrEmpty(phim.IdPhim))
                {
                    phim.IdPhim = new PhimIdGenerator().Next(null);
                }

                if (_context.Phims.Any(e => e.IdPhim == phim.IdPhim))
                {
                    return BadRequest("ID Phim đã tồn tại.");
                }

                var theloai = await _context.LoaiPhims.FirstOrDefaultAsync(x => x.TenLoai == phim.TheLoai);

                if (theloai == null)
                {
                    return BadRequest("Thể loại không tồn tại.");
                }

                phim.TheLoai = theloai.IdLP; 

                _context.Phims.Add(phim);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPhim), new { id = phim.IdPhim }, phim);
            }


        // PUT: api/Phim/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePhim(string id, [FromBody] Phim updatedPhim)
        {
            // Check if the record with the provided ID exists
            if (!await _context.Phims.AnyAsync(e => e.IdPhim == id))
            {
                return BadRequest("ID không tồn tại.");
            }

            var theloai = await _context.LoaiPhims.FirstOrDefaultAsync(x => x.TenLoai == updatedPhim.TheLoai);
            if (theloai == null)
            {
                return BadRequest("Thể loại không tồn tại.");
            }

            var existingPhim = await _context.Phims.FindAsync(id);
            if (existingPhim == null)
            {
                return NotFound();
            }

            existingPhim.TenPhim = updatedPhim.TenPhim;
            existingPhim.DienVien = updatedPhim.DienVien;
            existingPhim.DangPhim = updatedPhim.DangPhim;
            existingPhim.TheLoai = theloai.IdLP;
            existingPhim.ThoiLuong = updatedPhim.ThoiLuong;
            existingPhim.HinhAnh = updatedPhim.HinhAnh;

            _context.Entry(existingPhim).State = EntityState.Modified;

            try
            {
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
