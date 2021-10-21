﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Football_Manager.Models
{
    public class Position
    {
        public int PositionId { get; set; }
        public string Name { get; set; }
        public int? DisplayOrder { get; set; }

        //public int PlayerId { get; set; }
        //public ICollection<Player> Players { get; set; }
    }
}
