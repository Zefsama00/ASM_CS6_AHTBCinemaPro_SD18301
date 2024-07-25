
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Models
{
    public class GioChieu
    {
        [Key]
        public int IdGioChieu { get; set; }

        [Required(ErrorMessage = "Giờ bắt đầu là bắt buộc.")]
        [CustomValidation(typeof(GioChieu), "ValidateGioBatDau")]
        public TimeSpan GioBatDau { get; set; }

        [Required(ErrorMessage = "Giờ kết thúc là bắt buộc.")]
        public TimeSpan GioKetThuc { get; set; }

        [ForeignKey("CaChieus")]
        public int Cachieu { get; set; }
        public CaChieu CaChieus { get; set; }

        public string TrangThai { get; set; }

        public ICollection<Ve> Ves { get; set; }

        // Static method to perform custom validation for GioBatDau
        public static ValidationResult ValidateGioBatDau(TimeSpan gioBatDau, ValidationContext context)
        {
            var gioChieu = context.ObjectInstance as GioChieu;

            if (gioChieu != null && gioBatDau >= gioChieu.GioKetThuc)
            {
                return new ValidationResult("Giờ bắt đầu phải nhỏ hơn giờ kết thúc.");
            }

            return ValidationResult.Success;
        }
    }
}
