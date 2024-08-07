using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using ASM_CS6_AHTBCinemaPro_SD18301.Model;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using ASM_CS6_AHTBCinemaPro_SD18301.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NgayChieusController : ControllerBase
    {
        private readonly DBCinemaContext _context;

        public NgayChieusController(DBCinemaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NgayChieuVM>>> GetNgayChieus()
        {
            var ngayChieus = await _context.NgayChieus
                .Include(nc => nc.Phims)
                .Include(nc => nc.Phongs)
                .ToListAsync();

            var currentTime = DateTime.Now;

            var result = ngayChieus.Select(nc => new NgayChieuVM
            {
                IdCaChieu = nc.IdCaChieu,
                NgayChieu = nc.NgayChieuPhim,
                Phim = nc.Phims.TenPhim,
                Phong = nc.Phongs.SoPhong,
                TrangThai = GetTrangThai(nc.NgayChieuPhim, currentTime)
            }).ToList();

            // Cập nhật trạng thái trong cơ sở dữ liệu nếu cần
            foreach (var ngayChieu in ngayChieus)
            {
                var newTrangThai = GetTrangThai(ngayChieu.NgayChieuPhim, currentTime);
                if (ngayChieu.TrangThai != newTrangThai)
                {
                    ngayChieu.TrangThai = newTrangThai;
                }
            }

            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<NgayChieu>> PostNgayChieu(NgayChieuVM ngayChieuVM)
        {
            var ngayChieu = new NgayChieu
            {
                IdCaChieu = ngayChieuVM.IdCaChieu,
                NgayChieuPhim = ngayChieuVM.NgayChieu,
                Phim = ngayChieuVM.Phim,
                Phong = ngayChieuVM.IdPhong,
                TrangThai = GetTrangThai(ngayChieuVM.NgayChieu, DateTime.Now)
            };

            _context.NgayChieus.Add(ngayChieu);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNgayChieuById), new { id = ngayChieu.IdCaChieu }, ngayChieu);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNgayChieu(int id, NgayChieuVM ngayChieuVM)
        {
            if (id != ngayChieuVM.IdCaChieu)
            {
                return BadRequest();
            }

            var ngayChieu = await _context.NgayChieus.FindAsync(id);

            if (ngayChieu == null)
            {
                return NotFound();
            }

            ngayChieu.NgayChieuPhim = ngayChieuVM.NgayChieu;
            ngayChieu.TrangThai = GetTrangThai(ngayChieuVM.NgayChieu, DateTime.Now);

            _context.Entry(ngayChieu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NgayChieuExists(id))
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

        [HttpGet("{id}")]
        public async Task<ActionResult<NgayChieu>> GetNgayChieuById(int id)
        {
            var ngayChieu = await _context.NgayChieus.FindAsync(id);

            if (ngayChieu == null)
            {
                return NotFound();
            }

            return ngayChieu;
        }

        private bool NgayChieuExists(int id)
        {
            return _context.NgayChieus.Any(e => e.IdCaChieu == id);
        }

        private static string GetTrangThai(DateTime ngayChieu, DateTime currentTime)
        {
            var ngaychieu = ngayChieu.Date;
            var currenttime = currentTime.Date;
            if (currenttime < ngaychieu)
            {
                return "Chưa Chiếu";
            }
            else if (currenttime == ngaychieu)
            {
                return "Đang Chiếu";
            }
            else
            {
                return "Hết Hạn";
            }
        }
    }
}