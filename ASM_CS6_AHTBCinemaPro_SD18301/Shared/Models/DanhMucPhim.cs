using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Shared.Models
{
    public class DanhMucPhim
    {
        [Key]
        public int IDDanhMucPhim {  get; set; }
        [ForeignKey("Phim")]
        public string IdPhim { get; set; }
        public Phim Phim { get; set; }
        [ForeignKey("DanhMuc")]
        public int IdDanhMuc { get; set;}
        public DanhMuc DanhMuc { get; set; }

    }
}
