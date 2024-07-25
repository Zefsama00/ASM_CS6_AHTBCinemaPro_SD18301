using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Models
{
    public class Ghe
    {
        [Key]
        public string IdGhe { get; set; }
        public string TenGhe { get; set; }
        [ForeignKey("Phongs")]
        public string Phong { get; set; }
        public Phong Phongs {  get; set; }
        public string TrangThai { get; set; }
        [ForeignKey("LoaiGhes")]
        public string LoaiGhe { get; set; }
        public LoaiGhe LoaiGhes { get; set; }
        public Ve Ves { get; set; }
    }
}
