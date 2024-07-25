using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Models
{
    public class VeVM
    {
        public int IdVe { get; set; }
        public string TenVe { get; set; }
        public float GiaVe { get; set; }

        public int SuatChieu { get; set; }
   
        public string Ghe { get; set; }
    }
}
