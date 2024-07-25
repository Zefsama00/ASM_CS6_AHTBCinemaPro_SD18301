using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Models
{
    public class LoaiPhim
    {
        [Key]
        public string IdLP { get; set; }
        public string TenLoai { get; set; }
        public ICollection<Phim> Phims { get; set; }
    }
}
