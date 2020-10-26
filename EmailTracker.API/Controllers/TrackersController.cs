using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmailTracker.Data;
using EmailTracker.Domain.Entities;
using System.Net.Http;
using System.Net;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using System.IO;
using EmailTracker.Domain.Models.Responses;

namespace EmailTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackersController : ControllerBase
    {
        private readonly EmailTrackerContext _context;
        IConfiguration _configuration;
        public TrackersController(EmailTrackerContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Trackers
        [HttpGet]
        public ActionResult<IEnumerable<TrackerResponseModel>> GetTrackers()
        {

            var query = (from T in _context.Trackers
             join TE in _context.TrackerEvents
             on T.TrackerID equals TE.TrackerID into trackers
            from trackerevents in trackers.DefaultIfEmpty()
            select new TrackerResponseModel
             {
                 TrackerID = T.TrackerID,
                 ExternalID = T.ExternalID,
                 IPAddress = trackerevents.IPAddress ?? null
             }).ToList();

            return query;
        }

        // GET: api/Trackers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tracker>> GetTracker(int id)
        {
            var tracker = await _context.Trackers.FindAsync(id);

            if (tracker == null)
            {
                return NotFound();
            }

            return tracker;
        }

        // PUT: api/Trackers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTracker(int id, Tracker tracker)
        {
            if (id != tracker.TrackerID)
            {
                return BadRequest();
            }

            _context.Entry(tracker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackerExists(id))
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

        // POST: api/Trackers/SendEmail
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("SendEmail")]
        public async Task<ActionResult<Tracker>> SendEmail(string emailAddress) // TODO: Pass in email object
        {
            try
            {
                var tracker = new Tracker();
                tracker.CreatedDate = DateTime.Now;
                tracker.ExternalID = Guid.NewGuid();
                _context.Trackers.Add(tracker);
                await _context.SaveChangesAsync();

                // create email message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_configuration.GetSection("SMTP:Email").Value));
                email.To.Add(MailboxAddress.Parse(emailAddress));
                email.Subject = "Email Tracker";
                email.Body = new TextPart(TextFormat.Html) { Text = $"<img id='imgTrack' src='https://localhost:44324/api/TrackerEvents/{tracker.ExternalID}'/>" };

                // send email
                using var smtp = new SmtpClient();
                // gmail
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_configuration.GetSection("SMTP:Email").Value, _configuration.GetSection("SMTP:Password").Value);
                smtp.Send(email);
                smtp.Disconnect(true);


                return CreatedAtAction("GetTracker", new { id = tracker.TrackerID }, tracker);
            }
            catch (Exception ex)
            {
                //TODO: Set up error log (ex. airbrake, custom, etc)
                return BadRequest(ex.Message);
            }
        }


        // DELETE: api/Trackers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tracker>> DeleteTracker(int id)
        {
            var tracker = await _context.Trackers.FindAsync(id);
            if (tracker == null)
            {
                return NotFound();
            }

            _context.Trackers.Remove(tracker);
            await _context.SaveChangesAsync();

            return tracker;
        }

        private bool TrackerExists(int id)
        {
            return _context.Trackers.Any(e => e.TrackerID == id);
        }     
    }
}
