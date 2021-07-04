﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Greenhose : Model
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public List<GardenBed> Beds { get; set; }
    }
}
