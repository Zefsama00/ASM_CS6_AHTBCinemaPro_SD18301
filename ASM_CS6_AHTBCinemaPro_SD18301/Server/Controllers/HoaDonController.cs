using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly DBCinemaContext _context;

        public HoaDonController(DBCinemaContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("dshoadon")]
        public async Task<ActionResult<IEnumerable<HoaDonVM>>> gethoadon()
        {
            var hoaDons = await _context.HoaDons
                .Include(h => h.Ve)
                .Include(h => h.NhanViens)
                .Include(h => h.KhachHangs)
                .Include(h => h.KhuyenMais)
                .Select(h => new HoaDonVM
                {
                    IdHD = h.IdHD,
                    IdVe = h.IdVe,
                    NhanVien = h.NhanViens != null ? h.NhanViens.TenNV : h.NhanVien, // Adjust based on your actual property
                    KhachHang = h.KhachHangs != null ? h.KhachHangs.TenKH : h.KhachHang, // Adjust based on your actual property
                    KhuyenMai = h.KhuyenMais != null ? h.KhuyenMais.IdKM : h.KhuyenMai, // Adjust based on your actual property
                    TongTien = h.TongTien,
                    TrangThai = h.TrangThai
                })
                .ToListAsync();

            return Ok(hoaDons);
        }

    }
}