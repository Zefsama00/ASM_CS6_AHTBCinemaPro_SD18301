using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using ASM_CS6_AHTBCinemaPro_SD18301.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        var ngayChieuCuaPhim = await _context.NgayChieus
            .Include(c => c.Phongs)
            .FirstOrDefaultAsync(c => c.Phim == id);

        List<GioChieu> cacCaChieuCuaPhim = new List<GioChieu>();
        List<Ghe> ghelist = new List<Ghe>();

        if (ngayChieuCuaPhim != null)
        {
            cacCaChieuCuaPhim = await _context.GioChieus
                .Where(c => c.Cachieu == ngayChieuCuaPhim.IdCaChieu)
                .ToListAsync();

            if (ngayChieuCuaPhim.Phongs != null)
            {
                ghelist = await _context.Ghes
                    .Where(g => g.Phong == ngayChieuCuaPhim.Phongs.IdPhong)
                    .ToListAsync();
            }
        }

        var viewModel = new Multimodel
        {
            Phims = new List<Phim> { phim },
            Ghes = ghelist,
            NgayChieus = ngayChieuCuaPhim != null ? new List<NgayChieu> { ngayChieuCuaPhim } : new List<NgayChieu>(),
            GioChieus = cacCaChieuCuaPhim
        };

        return Ok(viewModel);
    }

    [HttpGet]
    [Route("LoadGhe/{id}")]
    public IActionResult LoadGhe(int id)
    {
        var veList = _context.Ves.Where(v => v.SuatChieu == id).ToList();
        var gheIds = veList.Select(v => v.Ghe).ToList();
        var seatNames = _context.Ghes
            .Where(g => gheIds.Contains(g.IdGhe))
            .Select(g => new { Id = g.IdGhe, Name = g.TenGhe })
            .ToList();

        return Ok(seatNames);
    }
    [HttpPost("CreateInvoice")]
    public async Task<IActionResult> CreateInvoice([FromBody] ThanhToanRequest request)
    {
        try
        {
            // Lấy thông tin phim và vé từ database
            var phim = await _context.Phims.FirstOrDefaultAsync(f => f.IdPhim == request.IdPhim);
            if (phim == null )
            {
                return BadRequest("Thông tin phim không hợp lệ.");
            }
            // Lấy thông tin khách hàng dựa trên IdUser
            var khachHang = await _context.KhachHangs
                            .Include(kh => kh.Users) 
                            .FirstOrDefaultAsync(kh => kh.IDUser == request.idUser);

            if (khachHang == null)
            {
                return BadRequest("Không tìm thấy thông tin khách hàng.");
            }

            // Tạo hóa đơn mới
            var hoaDon = new HoaDon
            {
                IdVe = request.IdVe,
                NhanVien = "NV01", // Tạm thời
                KhachHang = khachHang.IdKH, // Liên kết với IdKhachHang
                KhuyenMai = null,
                TongTien = request.TongTien,
                TrangThai = "Chưa thanh toán"
            };

            // Thêm hóa đơn vào context và lưu thay đổi
            _context.HoaDons.Add(hoaDon);
            await _context.SaveChangesAsync();

            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            // Log lỗi (nếu cần)
            return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
        }
    }


    [HttpPost("ThanhToan")]
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
            ghe.TrangThai = "Đã đặt";
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
