using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PantryAppAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PantryAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PantryItemsController : ControllerBase
    {
        private readonly IMongoCollection<PantryItem> _pantryItems;

        public PantryItemsController(PantryDbContext context)
        {
            _pantryItems = context.PantryItems;
        }

        // GET: api/pantryitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PantryItem>>> GetAll()
        {
            var items = await _pantryItems.Find(_ => true).ToListAsync();
            return Ok(items);
        }

        // GET: api/pantriitems/{id}
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<PantryItem>> Get(string id)
        {
            var item = await _pantryItems.Find(i => i.Id == id).FirstOrDefaultAsync();
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/pantryitems
        [HttpPost]
        public async Task<ActionResult> Create(PantryItem item)
        {
            await _pantryItems.InsertOneAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        // PUT: api/pantryitems/{id}
        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Update(string id, PantryItem updatedItem)
        {
            var result = await _pantryItems.ReplaceOneAsync(i => i.Id == id, updatedItem);
            if (result.MatchedCount == 0) return NotFound();
            return NoContent();
        }

        // DELETE: api/pantryitems/{id}
        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _pantryItems.DeleteOneAsync(i => i.Id == id);
            if (result.DeletedCount == 0) return NotFound();
            return NoContent();
        }
    }
}