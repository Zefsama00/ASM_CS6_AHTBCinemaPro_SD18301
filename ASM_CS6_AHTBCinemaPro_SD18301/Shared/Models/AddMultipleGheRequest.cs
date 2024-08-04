using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Shared.Models
{
    public class AddMultipleGheRequest
    {
        public string PhongId { get; set; }
        public int SoLuongGhe { get; set; }
        public string LoaiGhe { get; set; }
        public float GiaVe { get; set; }
        public int SuatChieu { get; set; }
    }
}
