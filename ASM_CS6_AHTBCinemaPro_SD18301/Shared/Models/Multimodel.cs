using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Shared.Models
{
    public class Multimodel
    {
        public List<Phim> Phims { get; set; }
        public List<Ghe> Ghes { get; set; }
        public List<NgayChieu> NgayChieus { get; set; }
        public List<GioChieu> GioChieus { get; set; }
        public List<Ve> Ves { get; set; }
        public float GiaVe { get; set; } 
    }
}