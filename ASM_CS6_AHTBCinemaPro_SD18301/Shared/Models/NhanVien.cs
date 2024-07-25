using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Models
{
    public class NhanVien
    {
        [Key]
        public string IdNV { get; set; }

        [Required(ErrorMessage = "Vui lòng không để trống tên Nhân Viên")]
        [StringLength(50, ErrorMessage = "Độ dài tối đa 50 ký tự")]
        public string TenNV { get; set; }

        [Required(ErrorMessage = "Vui lòng không để trống Số Điện Thoại")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Độ dài bắt buộc phải bằng 10")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Vui lòng nhập đúng chữ số 0-9")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Vui lòng không để trống email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ, a-z[0-9]@gmail.com")]
        [StringLength(50, ErrorMessage = "Độ dài tối đa 50 ký tự")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng không để trống Ngày Sinh")]
        [CustomValidation(typeof(NhanVien), nameof(ValidateDateOfBirth))]
        public DateTime NamSinh { get; set; }

        [Required(ErrorMessage = "Vui lòng không để trống Chức Vụ")]
        [StringLength(50, ErrorMessage = "Độ dài tối đa 50 ký tự")]
        public string ChucVu { get; set; }

        [Required(ErrorMessage = "Vui lòng không để trống mật khẩu")]
        public string Password { get; set; }

        public string TrangThai { get; set; } // Consider enum for defined states

        public string NgaySinhString => NamSinh.ToString("dd-MM-yyyy");
        [ForeignKey("Users")]
        public string IDUser { get; set; }
        public User Users { get; set; }

        public ICollection<HoaDon> HoaDons { get; set; }

        public static ValidationResult ValidateDateOfBirth(DateTime date, ValidationContext context)
        {
            if (date > DateTime.Now)
            {
                return new ValidationResult("Ngày Sinh không thể ở tương lai");
            }
            if (DateTime.Now.Year - date.Year > 100)
            {
                return new ValidationResult("Ngày Sinh không hợp lệ, tuổi quá cao");
            }
            return ValidationResult.Success;
        }
    }
}
