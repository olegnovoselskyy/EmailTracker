using EmailTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmailTracker.Data
{
    public class EmailTrackerContext : DbContext
    {
        public EmailTrackerContext()
        {

        }

        public EmailTrackerContext(DbContextOptions<EmailTrackerContext> options):base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Tracker> Trackers { get; set; }
        public DbSet<TrackerEvent> TrackerEvents { get; set; }

    }
}
