using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using ASM_CS6_AHTBCinemaPro_SD18301.Shared.Models;
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
    public class ChiTietController : ControllerBase
    {
        private readonly DBCinemaContext _context;

        public ChiTietController(DBCinemaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetChiTiet/{id}")]
        public async Task<IActionResult> GetChiTiet(string id)
        {
            var phim = await _context.Phims
                .Include(p => p.LoaiPhim)
                .FirstOrDefaultAsync(m => m.IdPhim == id);

            if (phim == null)
            {
                return NotFound();
            }

            var ngayChieuCuaPhim = await _context.NgayChieus.Include(c => c.Phongs).FirstOrDefaultAsync(c => c.Phim == id);

            

            var cacCaChieuCuaPhim = await _context.GioChieus.Where(c => c.Cachieu == ngayChieuCuaPhim.IdCaChieu).ToListAsync();

            var ghelist = await _context.Ghes
            .Where(g => g.Phong == ngayChieuCuaPhim.Phongs.IdPhong)
            .ToListAsync();

            var viewModel = new Multimodel
            {
                Phims = new List<Phim> { phim },
                Ghes = ghelist,
                NgayChieus = new List<NgayChieu> { ngayChieuCuaPhim},
                GioChieus = cacCaChieuCuaPhim
            };

            return Ok(viewModel);
        }
        [HttpGet]
        [Route("LoadGhe/{id}")]
        public IActionResult LoadGhe(int id)
        {
            // Lấy danh sách các vé có SuatChieuId trùng với id
            var veList = _context.Ves.Where(v => v.SuatChieu == id).ToList();

            // Lấy danh sách các IdGhe từ danh sách vé
            var gheIds = veList.Select(v => v.Ghe).ToList();

            // Lấy danh sách tên ghế dựa trên các IdGhe
            var seatNames = _context.Ghes
                .Where(g => gheIds.Contains(g.IdGhe))
                .Select(g => new { Id = g.IdGhe, Name = g.TenGhe }) // Chỉ lấy Id và Name của ghế
                .ToList();

            return Ok(seatNames);
        }
        [HttpPost]
        public async Task<IActionResult> ThanhToan(string id, string idphim, int gioChieuId)
        {
            // Lấy thông tin phim
            var phim = await _context.Phims.FirstOrDefaultAsync(f => f.IdPhim == idphim);

            // Lấy thông tin ca chiếu dựa trên gioChieuId
            var cachieu = await _context.GioChieus
                .Include(c => c.CaChieus)
                .FirstOrDefaultAsync(x => x.IdGioChieu == gioChieuId);

            // Lấy thông tin giờ chiếu
            var giochieu = await _context.GioChieus.FirstOrDefaultAsync(n => n.IdGioChieu == gioChieuId);

            var ve = _context.Ves.FirstOrDefault(x => x.Ghe == id);

            // Cập nhật trạng thái ghế thành "đã có người đặt"
            var ghe = await _context.Ghes.FirstOrDefaultAsync(g => g.IdGhe == id);
            if (ghe != null)
            {
                ghe.TrangThai = "Đã có người đặt";
                _context.Ghes.Update(ghe);
                await _context.SaveChangesAsync();
            }

            // Chuẩn bị ViewModel để gửi tới view
            var viewModel = new Multimodel
            {
                NgayChieus = cachieu?.CaChieus != null ? new List<NgayChieu> { cachieu.CaChieus } : new List<NgayChieu>(),
                Ghes = await _context.Ghes.Where(g => g.IdGhe == id).ToListAsync(),
                GioChieus = new List<GioChieu> { giochieu },
                Phims = new List<Phim> { phim },
                Ves = new List<Ve> { ve },
            };

            return Ok(viewModel);
        }

    }
}