using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Shared.Models
{
    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
