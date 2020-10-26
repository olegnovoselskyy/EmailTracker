using EmailTracker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmailTracker.Domain.Entities
{
    public class Entity : IEntity
    {
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { get; set; }
    }
}
