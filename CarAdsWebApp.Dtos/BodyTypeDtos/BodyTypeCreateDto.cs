﻿using CarAdsWebApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Dtos
{
    public class BodyTypeCreateDto:IDto
    {
        public string Definition { get; set; }
    }
}
