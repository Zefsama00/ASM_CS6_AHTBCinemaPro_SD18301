using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Models
{
    public class HoaDonVM
    {
        public int IdHD { get; set; }
       
        public int IdVe { get; set; }
       
        public string Combo { get; set; }
        
        public string NhanVien { get; set; }
       
        public string KhachHang { get; set; }
        public int KhuyenMai { get; set; }
        public float TongTien { get; set; }
    }
}
