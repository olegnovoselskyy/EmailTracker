using System;
using System.Collections.Generic;
using System.Text;

namespace EmailTracker.Domain.Interfaces
{
    public interface IEntity
    {
        public DateTime CreatedDate { get; set; }
    }
}
