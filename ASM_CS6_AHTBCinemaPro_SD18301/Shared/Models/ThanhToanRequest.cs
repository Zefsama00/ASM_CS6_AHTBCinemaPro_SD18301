﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Shared.Models
{
    public class ThanhToanRequest
    {
        public string IdPhim { get; set; }
        public int GioChieuId { get; set; }
        public List<string> SelectedSeats { get; set; }
    }
}