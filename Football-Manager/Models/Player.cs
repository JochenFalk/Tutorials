using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Football_Manager.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public int? ShirtNo { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int? Appearances { get; set; }
        public int? Goals { get; set; }

        public decimal? GoalsPerMatch
        {
            get
            {
                if (this.Goals.HasValue &&  this.Appearances.HasValue)
                {
                    var goalsPerMatch = (decimal) Goals /  (decimal) Appearances;
                    return Math.Round(goalsPerMatch, 2);
                }
                else
                {
                    return null;
                }
            }
        }

        public Position Position { get; set; }
    }
}
