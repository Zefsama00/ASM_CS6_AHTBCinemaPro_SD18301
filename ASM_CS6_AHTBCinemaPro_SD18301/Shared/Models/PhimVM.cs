using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Model
{
    public class PhimVM
    {
       
        public string IdPhim { get; set; }

        [Required(ErrorMessage = "Tên Phim là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên Phim không được vượt quá 100 ký tự.")]
        public string TenPhim { get; set; }
        [Required(ErrorMessage = "Diễn viên là bắt buộc.")]

        [StringLength(100, ErrorMessage = "Diễn Viên không được vượt quá 100 ký tự.")]
        public string DienVien { get; set; }

        [Required(ErrorMessage = "Dạng Phim là bắt buộc.")]
        public string DangPhim { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Thời Lượng phải lớn hơn 0.")]
        public int ThoiLuong { get; set; }

        [StringLength(255, ErrorMessage = "Hình Ảnh không được vượt quá 255 ký tự.")]
        public string HinhAnh { get; set; }

        [StringLength(50, ErrorMessage = "Thể Loại không được vượt quá 50 ký tự.")]
        public string TheLoai { get; set; }

        [StringLength(50, ErrorMessage = "Trạng Thái không được vượt quá 50 ký tự.")]
        public string TrangThai { get; set; }
        public string IdTheLoai { get; set; }
    }
}
