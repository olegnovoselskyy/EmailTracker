using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmailTracker.Data;
using EmailTracker.Domain.Entities;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace EmailTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackerEventsController : ControllerBase
    {
        private readonly EmailTrackerContext _context;
        IConfiguration _configuration;

        public TrackerEventsController(EmailTrackerContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/TrackerEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrackerEvent>>> GetTrackerEvents()
        {
            return await _context.TrackerEvents.ToListAsync();
        }

        // PUT: api/TrackerEvents/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrackerEvent(int id, TrackerEvent trackerEvent)
        {
            if (id != trackerEvent.EventID)
            {
                return BadRequest();
            }

            _context.Entry(trackerEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackerEventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TrackerEvents
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpGet("{trackerGuid}")]
        public IActionResult PostTrackerEvent(string trackerGuid)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            var whiteListedIPs = _configuration.GetSection("WhiteListedIPs");
            if (!whiteListedIPs.GetChildren().Any(item => item.Key == ipAddress))
            {
                var trackerId = _context.Trackers.Where(t => t.ExternalID.ToString() == trackerGuid)
                    .Select(i => i.TrackerID)
                    .FirstOrDefault();
                var trackerEvent = new TrackerEvent();
                trackerEvent.TrackerID = trackerId;
                trackerEvent.IPAddress = ipAddress;
                trackerEvent.CreatedDate = DateTime.Now;

                _context.TrackerEvents.Add(trackerEvent);
                _context.SaveChanges();

            }

            var directory = Directory.GetCurrentDirectory();
            var imagePath = directory + "\\images\\1px.png";
            var b = System.IO.File.ReadAllBytes(imagePath);
            return File(b, "image/png");

        }

        // DELETE: api/TrackerEvents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TrackerEvent>> DeleteTrackerEvent(int id)
        {
            var trackerEvent = await _context.TrackerEvents.FindAsync(id);
            if (trackerEvent == null)
            {
                return NotFound();
            }

            _context.TrackerEvents.Remove(trackerEvent);
            await _context.SaveChangesAsync();

            return trackerEvent;
        }

        private bool TrackerEventExists(int id)
        {
            return _context.TrackerEvents.Any(e => e.EventID == id);
        }
    }
}
