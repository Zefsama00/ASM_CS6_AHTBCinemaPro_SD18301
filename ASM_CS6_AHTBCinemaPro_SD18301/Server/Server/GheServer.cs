using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Server.Server
{
    public class GheServer
    {
        private readonly DBCinemaContext _context;
        private readonly ILogger<GheServer> _logger;
        private List<Ghe> _ghe;

        public GheServer(DBCinemaContext context, ILogger<GheServer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task LoadGhesAsync()
        {
           
           _ghe = await _context.Ghes.ToListAsync();
                    
        }

        public List<Ghe> GetGhes()
        {
            return _context.Ghes.ToList();
        }
    }
}
