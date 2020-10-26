using System;
using System.Collections.Generic;
using System.Text;

namespace EmailTracker.Domain.Models.Responses
{
    public class TrackerResponseModel
    {
        public int TrackerID { get; set; }
        public Guid ExternalID { get; set; }
        public string IPAddress { get; set; }
    }
}
