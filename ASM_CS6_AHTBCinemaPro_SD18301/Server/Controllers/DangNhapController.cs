using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using ASM_CS6_AHTBCinemaPro_SD18301.Shared.Models;
using ASM_CS6_AHTBCinemaPro_SD18301.Client.Service;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Server.Controllers
{
        
    [Route("api/[controller]")]
    [ApiController]
    public class DangNhapController : ControllerBase
    {
        private readonly DBCinemaContext _context;
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;
        public DangNhapController(DBCinemaContext context, IConfiguration configuration, EmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
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
        private string GeneratePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] KhachHang khachHang)
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
                    Username = khachHang.Email.Substring(0, khachHang.Email.IndexOf("@")),
                    PassWord = GetMd5Hash(khachHang.Password),
                    Role = "User"
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                string lastIdKH = await _context.KhachHangs.OrderByDescending(kh => kh.IdKH).Select(kh => kh.IdKH).FirstOrDefaultAsync();
                int nextIdKH = (lastIdKH == null) ? 1 : int.Parse(lastIdKH.Substring(2)) + 1;
                khachHang.IdKH = "KH" + nextIdKH.ToString("D2");
                khachHang.TrangThai = "Hoạt động";
                khachHang.IDUser = user.IdUser;

                _context.KhachHangs.Add(khachHang);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Đăng ký thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi lưu dữ liệu. Vui lòng thử lại sau.", error = ex.Message });
            }
        }

        [HttpPost("LogIn")]
        public IActionResult LogIn([FromBody] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = GetMd5Hash(loginModel.Password);

                var u = _context.Users.FirstOrDefault(x => x.Username == loginModel.Username && x.PassWord == hashedPassword);
                if (u != null)
                {
                    // Return a token and UserId
                    return Ok(new { Token = GenerateJwtToken(u), UserId = u.IdUser });
                }
                else
                {
                    return Unauthorized(new { Message = "Tài khoản hoặc mật khẩu không chính xác." });
                }
            }

            return BadRequest(ModelState);
        }
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgetPasswordRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return BadRequest("Xảy ra lỗi ở request");
            }
            var khachHang = await _context.KhachHangs.FirstOrDefaultAsync(k => k.Email == request.Email);
            if (khachHang == null)
            {
                return NotFound("khachHang không tồn tại.");

            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.IdUser  == khachHang.IDUser);
            if (user == null)
            {
                return NotFound("User không tồn tại.");
            }
            string newPassword = GeneratePassword(8);
            user.PassWord = GetMd5Hash(newPassword);
            khachHang.Password = GetMd5Hash(newPassword);
            _context.Users.Update(user);
            _context.KhachHangs.Update(khachHang);
            await _context.SaveChangesAsync();

            await _emailService.SendEmailAsync(khachHang.Email, "Quên mật khẩu", $"Mật khẩu mới của bạn là: {newPassword}");

            return Ok("Mật khẩu mới đã được gửi qua email.");
        }


        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role),
        new Claim(ClaimTypes.NameIdentifier, user.IdUser) // Add UserId here
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
