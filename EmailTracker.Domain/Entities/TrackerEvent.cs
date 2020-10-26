using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmailTracker.Domain.Entities
{
    public class TrackerEvent : Entity
    {
        [Key]
        public int EventID { get; set; }
        [Required]
        public int TrackerID { get; set; }
        public Tracker Tracker { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(45)")]
        public string IPAddress { get; set; }
    }
}
