using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Model
{
    public class PhimVM
    {
        
        public string IdPhim { get; set; }
        public string TenPhim { get; set; }
        public string DienVien { get; set; }
        public string DangPhim { get; set; }
        public int ThoiLuong { get; set; }
        public string HinhAnh { get; set; }
        public string TheLoai { get; set; }
    }
}
