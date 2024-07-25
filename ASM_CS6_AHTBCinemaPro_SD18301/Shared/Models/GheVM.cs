using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Models
{
    public class GheVM
    {
        public string IdGhe { get; set; }
        public string TenGhe { get; set; }
       
        public string Phong { get; set; }
        public string TrangThai { get; set; }

        public string LoaiGhe { get; set; }
    }
}
