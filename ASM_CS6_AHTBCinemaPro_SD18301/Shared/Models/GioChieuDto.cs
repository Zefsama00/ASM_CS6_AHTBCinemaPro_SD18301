using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Shared.Models
{
    public class GioChieuDto
    {
        public int IdGioChieu { get; set; }
        public string GioBatDau { get; set; } // Chuỗi định dạng "hh:mm"
        public string GioKetThuc { get; set; } // Chuỗi định dạng "hh:mm"
        public string TrangThai { get; set; }
    }
}
