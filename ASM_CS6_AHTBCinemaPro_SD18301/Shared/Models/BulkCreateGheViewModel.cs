using ASM_CS6_AHTBCinemaPro_SD18301.Shared.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Models
{
    public class BulkCreateGheViewModel
    {
        [Required(ErrorMessage = "Vui lòng không để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng nhập một số nguyên dương.")]
        //[ValidateGheQuantity(ErrorMessage = "Số lượng ghế thêm vào vượt quá số lượng ghế có sẵn trong phòng.")]
        public int SoLuongGhe { get; set; }

        [Required(ErrorMessage = "Vui lòng không để trống.")]
        public string Phong { get; set; }
        public string TenGhe { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống.")]
        public string TrangThai { get; set; }

        [Required(ErrorMessage = "Vui lòng không để trống.")]
        public string LoaiGhe { get; set; }

        [Required(ErrorMessage = "Vui lòng không để trống.")]
        [RegularExpression("^[A-Z]$", ErrorMessage = "Vui lòng nhập một chữ cái viết hoa duy nhất.")]
        public string StartingSeatLetter { get; set; }

        [Required(ErrorMessage = "Vui lòng không để trống.")]
        [Range(1, double.MaxValue, ErrorMessage = "Vui lòng nhập một số dương.")]
        public float GiaVe { get; set; }

        public int? GioChieuId { get; set; }
        public int idVe {  get; set; }
    }
}
