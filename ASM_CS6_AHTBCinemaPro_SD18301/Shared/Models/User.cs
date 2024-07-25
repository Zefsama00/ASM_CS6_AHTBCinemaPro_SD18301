using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Models
{
    public class User
    {
        [Key]
        public string IdUser { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string PassWord { get; set; }
        public string Role { get; set; }
        public ICollection<NhanVien> NhanViens { get; set; }
        public ICollection<KhachHang> KhachHangs { get; set; }
    }
}
