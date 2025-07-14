using CRUD_ForoUTTN.Models;
using CRUD_ForoUTTN.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;


namespace CRUD_ForoUTTN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionsController : ControllerBase
    {
        private readonly MongoService _mongoService;

        public ActionsController(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        // GET: api/Actions
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var actions = await _mongoService.Actions.Find(Builders<Actionn>.Filter.Empty).ToListAsync();
            return Ok(actions);
        }

        // GET: api/Actions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var action = await _mongoService.Actions.Find(a => a.Id == id).FirstOrDefaultAsync();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        // POST: api/Actions
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Actionn newAction)
        {
            if (newAction == null)
            {
                return BadRequest("Datos de acción inválidos.");
            }

            await _mongoService.Actions.InsertOneAsync(newAction);
            return CreatedAtAction(nameof(Get), new { id = newAction.Id }, newAction);
        }

        // PUT: api/Actions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Actionn updatedAction)
        {
            var result = await _mongoService.Actions.ReplaceOneAsync(a => a.Id == id, updatedAction);
            if (result.MatchedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/Actions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mongoService.Actions.DeleteOneAsync(a => a.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

