using ASM_CS6_AHTBCinemaPro_SD18301.Data;
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
    public class GioChieuController : ControllerBase
    {
        private readonly DBCinemaContext _context;

        public GioChieuController(DBCinemaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetGioChieus()
        {
            var currentTime = DateTime.Now;

            var gioChieus = await _context.GioChieus
                .Include(g => g.CaChieus)
                    .ThenInclude(c => c.Phims)
                .Include(g => g.CaChieus)
                    .ThenInclude(c => c.Phongs)
                .ToListAsync();

            bool hasChanges = false;

            var gioChieuViewModels = gioChieus.Select(g =>
            {
                var trangThai = GetTrangThai(g.CaChieus.NgayChieuPhim, g.GioBatDau, g.GioKetThuc, currentTime);
                if (g.TrangThai != trangThai)
                {
                    g.TrangThai = trangThai;
                    hasChanges = true;
                }

                return new GioChieuViewModel
                {
                    Cachieu = g.CaChieus.Phims.TenPhim + " - " + " Số phòng " + g.CaChieus.Phongs.SoPhong + " - " + g.CaChieus.NgayChieuPhim.ToString("dd/MM/yyyy"),
                    IdGioChieu = g.IdGioChieu,
                    GioBatDau = g.GioBatDau.ToString(@"hh\:mm"), // Chuyển đổi TimeSpan thành giờ và phút
                    GioKetThuc = g.GioKetThuc.ToString(@"hh\:mm"),
                    TrangThai = g.TrangThai
                };
            }).ToList();

            if (hasChanges)
            {
                await _context.SaveChangesAsync();
            }

            return Ok(gioChieuViewModels);
        }

        public static string GetTrangThai(DateTime ngayChieu, TimeSpan gioBatDau, TimeSpan gioKetThuc, DateTime currentTime)
        {
            var startDateTime = ngayChieu.Date.Add(gioBatDau);
            var endDateTime = ngayChieu.Date.Add(gioKetThuc);

            if (currentTime < startDateTime)
            {
                return "Chưa chiếu";
            }
            else if (currentTime >= startDateTime && currentTime <= endDateTime)
            {
                return "Đang chiếu";
            }
            else
            {
                return "Hết hạn";
            }
        }



        [HttpPost]
        public async Task<IActionResult> AddGioChieu([FromBody] GioChieuViewModel gioChieuViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TimeSpan.TryParse(gioChieuViewModel.GioBatDau, out TimeSpan gioBatDau) ||
                !TimeSpan.TryParse(gioChieuViewModel.GioKetThuc, out TimeSpan gioKetThuc) ||
                gioBatDau >= gioKetThuc)
            {
                return BadRequest("Thời gian không hợp lệ.");
            }

            var gioChieu = new GioChieu
            {
                GioBatDau = gioBatDau,
                GioKetThuc = gioKetThuc,
                Cachieu = gioChieuViewModel.CaChieuId,
                TrangThai = gioBatDau > DateTime.Now.TimeOfDay ? "Chưa chiếu" :
                            gioBatDau <= DateTime.Now.TimeOfDay && gioKetThuc >= DateTime.Now.TimeOfDay ? "Đang chiếu" :
                            "Hết hạn"
            };

            _context.GioChieus.Add(gioChieu);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGioChieus), new { id = gioChieu.IdGioChieu }, gioChieu);
        }

        // Thêm hành động PUT để cập nhật giờ chiếu
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGioChieu(int id, [FromBody] GioChieuViewModel gioChieuViewModel)
        {
            if (id != gioChieuViewModel.IdGioChieu)
            {
                return BadRequest("ID giờ chiếu không khớp.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TimeSpan.TryParse(gioChieuViewModel.GioBatDau, out TimeSpan gioBatDau) ||
                !TimeSpan.TryParse(gioChieuViewModel.GioKetThuc, out TimeSpan gioKetThuc) ||
                gioBatDau >= gioKetThuc)
            {
                return BadRequest("Thời gian không hợp lệ.");
            }

            var gioChieu = await _context.GioChieus.FindAsync(id);
            if (gioChieu == null)
            {
                return NotFound();
            }

            gioChieu.GioBatDau = gioBatDau;
            gioChieu.GioKetThuc = gioKetThuc;
            gioChieu.TrangThai = gioBatDau > DateTime.Now.TimeOfDay ? "Chưa chiếu" :
                                gioBatDau <= DateTime.Now.TimeOfDay && gioKetThuc >= DateTime.Now.TimeOfDay ? "Đang chiếu" :
                                "Hết hạn";

            _context.Entry(gioChieu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GioChieuExists(id))
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

        private bool GioChieuExists(int id)
        {
            return _context.GioChieus.Any(e => e.IdGioChieu == id);
        }
    }
}