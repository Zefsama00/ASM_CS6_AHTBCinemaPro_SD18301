using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Models
{
    public class LoaiGhe
    {
        [Key]
        public string IdLoaiGhe { get; set; }
        public string TenLoaiGhe { get; set; }
        public ICollection<Ghe> Ghes { get; set; }
    }
}
