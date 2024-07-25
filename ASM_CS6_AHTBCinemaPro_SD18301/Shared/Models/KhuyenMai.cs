
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Models
{
    public class KhuyenMai
    {
        [Key] 
        public int IdKM { get; set; }
        public string KhuyenMaiName { get; set; }
        public int Phantram { get; set; }
        public ICollection<HoaDon> HoaDons { get; set; }
    }
}
