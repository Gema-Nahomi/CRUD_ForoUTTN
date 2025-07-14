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
    public class LoginController : ControllerBase
    {
        private readonly MongoService _mongoService;

        public LoginController(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var logins = await _mongoService.Login.Find(Builders<Login>.Filter.Empty).ToListAsync();
            return Ok(logins);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var login = await _mongoService.Login.Find(l => l.Id == new ObjectId(id)).FirstOrDefaultAsync();
            if (login == null)
            {
                return NotFound();
            }
            return Ok(login);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Login newLogin)
        {
            await _mongoService.Login.InsertOneAsync(newLogin);
            return CreatedAtAction(nameof(Get), new { id = newLogin.Id }, newLogin);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Login updatedLogin)
        {
            var result = await _mongoService.Login.ReplaceOneAsync(l => l.Id == new ObjectId(id), updatedLogin);
            if (result.MatchedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mongoService.Login.DeleteOneAsync(l => l.Id == new ObjectId(id));
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
