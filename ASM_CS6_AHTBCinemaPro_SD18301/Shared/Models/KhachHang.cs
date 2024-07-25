using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Models
{
    public class KhachHang
    {
        [Key]
        public string IdKH { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống tên Khách Hàng")]
        [StringLength(50, ErrorMessage = "Độ dài tối đa là 50")]
        public string TenKH { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống Số Điện Thoại")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Độ dài bắt buộc phải bằng 10")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Vui lòng nhập đúng chữ số 0-9")]
        public string SDT { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống Ngày Sinh")]
        public DateTime NamSinh { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ, a-z[0-9]@gmail.com")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống mật khẩu")]

        public string Password { get; set; }
        public string TrangThai { get; set; }
        public string NgaySinhString => NamSinh.ToString("dd-MM-yyyy");
        [ForeignKey("Users")]
        public string IDUser { get; set; }
        public User Users { get; set; }
        public ICollection<HoaDon> HoaDons { get; set; }
    }
}
