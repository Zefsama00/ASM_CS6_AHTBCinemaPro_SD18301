using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Models
{
    public class Phong
    {
        [Key]
        public string IdPhong { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoPhong { get; set; }

        [Required(ErrorMessage = "Vui lòng không để trống trạng thái")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Trạng thái chỉ được chứa chữ cái và dấu cách, không bao gồm số hoặc ký tự đặc biệt")]
        public string TrangThai { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuongGhe { get; set; }

        public ICollection<Ghe> Ghes { get; set; }
        public ICollection<NgayChieu> CaChieu { get; set; }
    }

}
