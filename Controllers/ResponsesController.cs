using CRUD_ForoUTTN.Models;
using CRUD_ForoUTTN.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;

namespace CRUD_ForoUTTN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponsesController : ControllerBase
    {
        private readonly MongoService _mongoService;

        public ResponsesController(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var responses = await _mongoService.Responses.Find(Builders<Response>.Filter.Empty).ToListAsync();
            return Ok(responses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _mongoService.Responses.Find(r => r.Id == id).FirstOrDefaultAsync();
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Response newResponse)
        {
            if (newResponse == null)
            {
                return BadRequest("Response data cannot be null");
            }

            await _mongoService.Responses.InsertOneAsync(newResponse);
            return CreatedAtAction(nameof(GetById), new { id = newResponse.Id }, newResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Response updatedResponse)
        {
            if (updatedResponse == null)
            {
                return BadRequest("Response data cannot be null");
            }

            var result = await _mongoService.Responses.ReplaceOneAsync(r => r.Id == id, updatedResponse);
            if (result.MatchedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mongoService.Responses.DeleteOneAsync(r => r.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

