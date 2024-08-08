using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly DBCinemaContext _context;

        public NhanVienController(DBCinemaContext context)
        {
            _context = context;
        }

        // GET: api/NhanVien
        [HttpGet("nhanvien")]
        public async Task<ActionResult<IEnumerable<NhanVien>>> GetNhanViens()
        {
            var dsnhanvien = await _context.NhanViens.ToListAsync();
            return Ok(dsnhanvien);
        }

        // GET: api/NhanVien/{id}
        [HttpGet("{id}")]
        [Route("edit")]
        public async Task<ActionResult<NhanVien>> GetNhanVien(string id)
        {
            var nhanVien = await _context.NhanViens.FindAsync(id);

            if (nhanVien == null)
            {
                return NotFound();
            }

            return nhanVien;
        }

        // POST: api/NhanVien
        [HttpPost]
        public async Task<IActionResult> AddNhanVien([FromBody] NhanVien nhanVien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string lastIdUser = await _context.Users.OrderByDescending(u => u.IdUser).Select(u => u.IdUser).FirstOrDefaultAsync();
                int nextIdUser = (lastIdUser == null) ? 1 : int.Parse(lastIdUser.Substring(2)) + 1;

                var user = new User
                {
                    IdUser = "US" + nextIdUser.ToString("D2"),
                    Username = nhanVien.Email.Substring(0, nhanVien.Email.IndexOf("@")),
                    PassWord = GetMd5Hash(nhanVien.Password),
                    Role = "NhanVien"
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                string lastIdNV = await _context.NhanViens.OrderByDescending(nv => nv.IdNV).Select(nv => nv.IdNV).FirstOrDefaultAsync();
                int nextIdNV = (lastIdNV == null) ? 1 : int.Parse(lastIdNV.Substring(2)) + 1;
                nhanVien.IdNV = "NV" + nextIdNV.ToString("D2");
                nhanVien.TrangThai = "Hoạt động";
                nhanVien.IDUser = user.IdUser;

                _context.NhanViens.Add(nhanVien);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Nhân viên đã được thêm thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi lưu dữ liệu. Vui lòng thử lại sau.", error = ex.Message });
            }
        }

        // PUT: api/NhanVien/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNhanVien(string id, [FromBody] NhanVien updatedNV)
        {
            if (id != updatedNV.IdNV)
            {
                return BadRequest();
            }

            var existingNhanVien = await _context.NhanViens.FindAsync(id);
            if (existingNhanVien == null)
            {
                return NotFound();
            }

            // Only update ChucVu
 
            existingNhanVien.ChucVu = updatedNV.ChucVu;
           

            _context.Entry(existingNhanVien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhanVienExists(id))
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



        private bool NhanVienExists(string id)
        {
            return _context.NhanViens.Any(e => e.IdNV == id);
        }

        private string GetMd5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
