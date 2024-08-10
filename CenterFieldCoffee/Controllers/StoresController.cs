using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CenterFieldCoffee.Data;
using CenterFieldCoffee.Models;

namespace CenterFieldCoffee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly CenterFieldCoffeeContext _context;

        public StoresController(CenterFieldCoffeeContext context)
        {
            _context = context;
        }

        // GET: api/Stores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetStore(string? currtime)
        {
            var currHour = string.IsNullOrEmpty(currtime) ? (DateTime.Now.Hour * 60) + DateTime.Now.Minute : (DateTimeOffset.Parse(currtime).UtcDateTime.Hour * 60) + DateTimeOffset.Parse(currtime).UtcDateTime.Minute;
            return await _context.Store
                .Select(x => new Store
                {
                    Id = x.Id,
                    Name = x.Name,
                    opening_time = $"{x.opening_hour}:{x.opening_minute.ToString("00")}",
                    closing_time = $"{x.closing_hour}:{x.closing_minute.ToString("00")}",
                    opening_hour = x.opening_hour,
                    opening_minute = x.opening_minute,
                    closing_hour = x.closing_hour,
                    closing_minute = x.closing_minute,
                    is_open = ((x.opening_hour * 60) + x.opening_minute) <= currHour && ((x.closing_hour * 60) + x.opening_minute) >= currHour
                })
                .ToListAsync();
        }

        // GET: api/Stores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStore(Guid id, string currtime)
        {
            var currHour = string.IsNullOrEmpty(currtime) ? (DateTime.Now.Hour * 60) + DateTime.Now.Minute : (DateTimeOffset.Parse(currtime).UtcDateTime.Hour * 60) + DateTimeOffset.Parse(currtime).UtcDateTime.Minute;
            var store = await _context.Store.FindAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            return new Store
            {
                Id = store.Id,
                Name = store.Name,
                opening_time = $"{store.opening_hour}:{store.opening_minute.ToString("00")}",
                closing_time = $"{store.closing_hour}:{store.closing_minute.ToString("00")}",
                opening_hour = store.opening_hour,
                opening_minute = store.opening_minute,
                closing_hour = store.closing_hour,
                closing_minute = store.closing_minute,
                is_open = ((store.opening_hour * 60) + store.opening_minute) <= currHour && ((store.closing_hour * 60) + store.opening_minute) >= currHour
            };
        }

        // PUT: api/Stores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStore(Guid id, Store store)
        {
            if (id != store.Id)
            {
                return BadRequest();
            }

            _context.Entry(store).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
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

        // POST: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Store>> PostStore(Store store)
        {
            _context.Store.Add(store);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStore", new { id = store.Id }, store);
        }

        // DELETE: api/Stores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(Guid id)
        {
            var store = await _context.Store.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            _context.Store.Remove(store);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoreExists(Guid id)
        {
            return _context.Store.Any(e => e.Id == id);
        }
    }
}
