using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using ASM_CS6_AHTBCinemaPro_SD18301.Server.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GheController : ControllerBase
    {
        private readonly GheServer _gheServer;
        private readonly DBCinemaContext _context;

        public GheController(GheServer gheServer, DBCinemaContext context)
        {
            _gheServer = gheServer;
            _context = context;
        }

        [HttpGet]
        [Route("GetGhe")]
        public List<Ghe> GetGhe()
        {
           
            var ghes = _context.Ghes.ToList();
            return ghes;

             
        }
    }
}
