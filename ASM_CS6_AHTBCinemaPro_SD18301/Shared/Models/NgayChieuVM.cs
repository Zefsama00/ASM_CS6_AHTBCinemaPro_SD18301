using System;
using System.ComponentModel.DataAnnotations;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Models
{
    public class NgayChieuVM
    {
        public string IdPhong { get; set; }

        public int IdCaChieu { get; set; }

        [Required(ErrorMessage = "Phòng không được để trống.")]
        public int Phong { get; set; }

        [Required(ErrorMessage = "Phim không được để trống.")]
        public string Phim { get; set; }

        [Required(ErrorMessage = "Ngày Chiếu không được để trống.")]
        public DateTime NgayChieu { get; set; }

        public string TrangThai { get; set; }
    }
}
