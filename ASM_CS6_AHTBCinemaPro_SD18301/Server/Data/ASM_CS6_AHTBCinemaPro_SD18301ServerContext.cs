using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Server.Data
{
    public class ASM_CS6_AHTBCinemaPro_SD18301ServerContext : DbContext
    {
        public ASM_CS6_AHTBCinemaPro_SD18301ServerContext (DbContextOptions<ASM_CS6_AHTBCinemaPro_SD18301ServerContext> options)
            : base(options)
        {
        }

        public DbSet<ASM_CS6_AHTBCinemaPro_SD18301.Models.NgayChieu> NgayChieu { get; set; }
    }
}
