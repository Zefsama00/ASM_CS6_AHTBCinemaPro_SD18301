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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhimController : ControllerBase
    {
        private readonly DBCinemaContext _context;
        private readonly IWebHostEnvironment _environment;

        public PhimController(DBCinemaContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
                        IdTheLoai = p.TheLoai,
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
        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image/Phim2");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { FileName = fileName }); // Trả về chỉ tên tệp
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

            // Lấy giá trị Id lớn nhất trong bảng LoaiPhims
            var maxId = _context.LoaiPhims
                .OrderByDescending(lp => lp.IdLP)
                .FirstOrDefault()?.IdLP;

            // Nếu bảng chưa có dữ liệu, đặt Id là 1, ngược lại tăng giá trị lên 1
            int nextIdNumber = 1;
            if (maxId != null && maxId.Length > 2 && int.TryParse(maxId.Substring(2), out int currentMaxId))
            {
                nextIdNumber = currentMaxId + 1;
            }
            string idloaiPhims = "LP" + nextIdNumber;
            // Tạo IdLP mới theo định dạng "LP" + số Id
            var theloai = new LoaiPhim
            {
                IdLP = idloaiPhims,
                TenLoai = phimvm.TheLoai
            };

            _context.LoaiPhims.Add(theloai);
            await _context.SaveChangesAsync();

            var phim = new Phim
            {
                IdPhim = phimvm.IdPhim,
                TenPhim = phimvm.TenPhim,
                ThoiLuong = phimvm.ThoiLuong,
                TheLoai = idloaiPhims,
                DienVien = phimvm.DienVien,
                HinhAnh = phimvm.HinhAnh,
                DangPhim = phimvm.DangPhim,
            };

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


            // Retrieve the existing movie (Phim) entity
            var existingPhim = await _context.Phims.FindAsync(id);
            if (existingPhim == null)
            {
                return NotFound();
            }
            var Loaip = _context.LoaiPhims.FirstOrDefault(x => x.IdLP == updatedPhimVM.IdTheLoai);
            if (Loaip != null)
            {
                Loaip.TenLoai = updatedPhimVM.TheLoai;
                await _context.SaveChangesAsync();
            }

            existingPhim.TenPhim = updatedPhimVM.TenPhim;
            existingPhim.DienVien = updatedPhimVM.DienVien;
            existingPhim.DangPhim = updatedPhimVM.DangPhim;
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