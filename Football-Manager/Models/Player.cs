using System;
using System.Collections.Generic;

#nullable disable

namespace Football_Manager.Models
{
    public partial class Player
    {
        public int Id { get; set; }
        public int? ShirtNo { get; set; }
        public string Name { get; set; }
        public int? PositionId { get; set; }
        public int? Appearances { get; set; }
        public int? Goals { get; set; }
    }
}
