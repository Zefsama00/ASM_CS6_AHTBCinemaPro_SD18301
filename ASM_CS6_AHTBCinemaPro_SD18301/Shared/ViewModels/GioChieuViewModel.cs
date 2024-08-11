using System;
using System.ComponentModel.DataAnnotations;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Shared.ViewModels
{
    public class GioChieuViewModel
    {
        public int IdGioChieu { get; set; }
        public int CaChieuId { get; set; }

        public string Cachieu { get; set; }

        [Required(ErrorMessage = "Giờ Bắt Đầu không được để trống.")]
        public string GioBatDau { get; set; } // Format giờ và phút

        [Required(ErrorMessage = "Giờ Kết Thúc không được để trống.")]
        public string GioKetThuc { get; set; } // Format giờ và phút

        public string TrangThai { get; set; }
    }
}
