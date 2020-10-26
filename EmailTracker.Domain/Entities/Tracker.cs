using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmailTracker.Domain.Entities
{
    public class Tracker : Entity
    {
        [Key]
        public int TrackerID { get; set; }
        [Required]
        public Guid ExternalID { get; set; }
    }
}
