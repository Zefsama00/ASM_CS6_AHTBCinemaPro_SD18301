﻿using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ASM_CS6_AHTBCinemaPro_SD18301.Data;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Models
{
    public class NgayChieu
    {
        [Key]
        public int IdCaChieu { get; set; }

        [ForeignKey("Phongs")]
        public string Phong { get; set; }
        public Phong Phongs { get; set; }

        [ForeignKey("Phims")]
        public string Phim { get; set; }
        public Phim Phims { get; set; }

        [Required(ErrorMessage = "Ngày chiếu là bắt buộc.")]
        [CustomValidation(typeof(NgayChieu), "ValidateUniqueCaChieu")]
        public DateTime NgayChieuPhim { get; set; }

        public string TrangThai { get; set; }

        public ICollection<GioChieu> GioChieus { get; set; }

        public static ValidationResult ValidateUniqueCaChieu(DateTime ngayChieu, ValidationContext context)
        {
            var caChieu = context.ObjectInstance as NgayChieu;
            var dbContext = context.GetService(typeof(DBCinemaContext)) as DBCinemaContext;

            var existingCaChieu = dbContext.NgayChieus
                .FirstOrDefault(c => c.Phim == caChieu.Phim && c.Phong == caChieu.Phong && c.NgayChieuPhim.Date == ngayChieu.Date && c.IdCaChieu != caChieu.IdCaChieu);

            if (existingCaChieu != null)
            {
                return new ValidationResult("Phim đã được chiếu trong phòng này vào ngày này.");
            }

            return ValidationResult.Success;
        }
    }
}
