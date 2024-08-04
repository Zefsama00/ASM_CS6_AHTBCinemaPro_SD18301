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
            var gioChieus = await _context.GioChieus
                .Select(g => new GioChieuViewModel
                {
                    Cachieu = g.CaChieus.Phims.TenPhim + " - " + " Số phòng "+ g.CaChieus.Phongs.SoPhong +" - " + g.CaChieus.NgayChieuPhim.ToString("dd/MM/yyyy"),
                    IdGioChieu = g.IdGioChieu,
                    GioBatDau = g.GioBatDau.ToString(@"hh\:mm"), // Chuyển đổi TimeSpan thành giờ và phút
                    GioKetThuc = g.GioKetThuc.ToString(@"hh\:mm"),
                    TrangThai = g.TrangThai
                })
                .ToListAsync();

            return Ok(gioChieus);
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
                TrangThai = gioChieuViewModel.TrangThai
            };

            _context.GioChieus.Add(gioChieu);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGioChieus), new { id = gioChieu.IdGioChieu }, gioChieu);
        }
    }
}
