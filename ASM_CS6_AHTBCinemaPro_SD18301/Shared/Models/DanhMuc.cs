using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Shared.Models
{
    public class DanhMuc
    {
        [Key]
        public int IdDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
        public ICollection<DanhMucPhim> DanhMucPhim { get; set; }
    }
}
