using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Shared.ViewModels
{
    public class GioChieuViewModel
    {
        public int IdGioChieu { get; set; }
        public int CaChieuId { get; set; }
        public string Cachieu { get; set; }
        public string GioBatDau { get; set; } // Format giờ và phút
        public string GioKetThuc { get; set; } // Format giờ và phút
        public string TrangThai { get; set; }
    }
}
