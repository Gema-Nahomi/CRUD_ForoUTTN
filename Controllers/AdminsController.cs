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
    public class AdminsController : ControllerBase
    {
        private readonly MongoService _mongoService;

        public AdminsController(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var admins = await _mongoService.Admins.Find(Builders<Admin>.Filter.Empty).ToListAsync();
            return Ok(admins);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var admin = await _mongoService.Admins.Find(a => a.Id == new ObjectId(id)).FirstOrDefaultAsync();
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Admin newAdmin)
        {
            await _mongoService.Admins.InsertOneAsync(newAdmin);
            return CreatedAtAction(nameof(Get), new { id = newAdmin.Id }, newAdmin);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Admin updatedAdmin)
        {
            var result = await _mongoService.Admins.ReplaceOneAsync(a => a.Id == new ObjectId(id), updatedAdmin);
            if (result.MatchedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mongoService.Admins.DeleteOneAsync(a => a.Id == new ObjectId(id));
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
