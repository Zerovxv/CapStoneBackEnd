using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CapStone.Modules;

namespace CapStone.Controllers
{








    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RequestsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests.Include(x => x.RequestLines).ThenInclude(x => x.Product).Include(x => x.User).SingleOrDefaultAsync(
                
                x => x.Id == id
                
                
                );

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        [HttpGet("reviews/{id}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetReviews(int id)
        {
            return await _context.Requests.Where(request => request.Status == "Review" && request.UserId != id)
                .ToListAsync();

        }

        [HttpPut("approve/{id}")]
        public async Task<IActionResult> PutRequests(int id, Request request)
        {
            if (request.Status == "APPROVED")
            {
                return BadRequest();
            }
            request.Status = "APPROVED";
            var rq = await PutRequests(id, request);



            return rq;

        }

        [HttpPut("reject/{id}")]
        public async Task<IActionResult> PutReject(int id, Request request)
        {
            if (request.Status == "REJECT")
            {
                return BadRequest();
            }
            request.Status = "REJECT";
            var rq = await PutReject(id, request);

            return rq;
        }


        [HttpPut("review")]

        public async Task<IActionResult> RequestReview(int id, Request request)
        {
            if (request.Total <= 50)
            {
                request.Status = "Approved";




            }
            else
            {
                request.Status = "Review";



            }

            return await PutRequest(id, request);
        }








        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
    
