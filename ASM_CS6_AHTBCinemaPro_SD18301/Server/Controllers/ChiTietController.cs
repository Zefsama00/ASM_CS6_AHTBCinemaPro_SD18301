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
        public IActionResult ThanhToan(string id, string idphim, int gioChieuId, string username)
        {
            // Tìm vé dựa trên id ghế
            var ve = _context.Ves.FirstOrDefault(x => x.Ghe == id);
            var phim = _context.Phims.FirstOrDefault(f => f.IdPhim == idphim);
            // Lấy thông tin ca chiếu dựa trên gioChieuId
            var cachieu = _context.GioChieus
                .Include(c => c.CaChieus) // Đảm bảo load thông tin phim
                .FirstOrDefault(x => x.IdGioChieu == gioChieuId);
            var userid = _context.Users.Where(x => x.Username == username).FirstOrDefault();

            //GioChieu
            var giochieu = _context.GioChieus.FirstOrDefault(n => n.IdGioChieu == gioChieuId);


            // Chuẩn bị ViewModel để gửi tới view
            var viewModel = new Multimodel
            {
                NgayChieus = new List<NgayChieu> { cachieu.CaChieus },
                Ghes = _context.Ghes.Where(g => g.IdGhe == id).ToList(),
                GioChieus = new List<GioChieu> { giochieu },
                Ves = new List<Ve> { ve },
                Phims = new List<Phim> { phim },
            };

            return Ok(viewModel);
        }
    }
}