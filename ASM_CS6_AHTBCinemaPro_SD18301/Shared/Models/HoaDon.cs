
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Models
{
    public class HoaDon
    {
        [Key]

        public int IdHD { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ", ApplyFormatInEditMode = false)]
        [ForeignKey("Ve")]
        public int IdVe { get; set; }
        public Ve Ve { get; set; }
        [ForeignKey("NhanViens")]
        public string NhanVien { get; set; }
        public NhanVien NhanViens { get; set; }
        [ForeignKey("KhachHangs")]
        public string KhachHang { get; set; }
        public KhachHang KhachHangs { get; set; }
        [ForeignKey("KhuyenMais")]
        public int? KhuyenMai { get; set; }
        public KhuyenMai KhuyenMais { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0} VNĐ", ApplyFormatInEditMode = false)]
        public float TongTien { get; set; }
        public string TrangThai { get; set; }

    }
}
