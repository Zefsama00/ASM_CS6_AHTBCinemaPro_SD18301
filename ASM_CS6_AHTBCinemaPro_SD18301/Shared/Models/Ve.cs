using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Models
{
    public class Ve
    {
        [Key]
        public int IdVe { get; set; }
        public string TenVe { get; set; }
        public float GiaVe { get; set; }
        [ForeignKey("GioChieus")]
        public int SuatChieu { get; set; }
        public GioChieu GioChieus {  get; set; }
        [ForeignKey("Ghes")]
        public string Ghe { get; set; }
        public Ghe Ghes { get; set; }
        public ICollection<HoaDon> HoaDons { get; set; }
    }
}
