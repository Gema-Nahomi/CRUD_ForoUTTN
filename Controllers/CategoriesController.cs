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
    public class CategoriesController : ControllerBase
    {
        private readonly MongoService _mongoService;

        public CategoriesController(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _mongoService.Categories.Find(Builders<Category>.Filter.Empty).ToListAsync();
            return Ok(categories);
        }

        // GET: api/Categories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var category = await _mongoService.Categories.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category newCategory)
        {
            if (newCategory == null)
            {
                return BadRequest("Invalid category data");
            }

            await _mongoService.Categories.InsertOneAsync(newCategory);
            return CreatedAtAction(nameof(Get), new { id = newCategory.Id }, newCategory);
        }

        // PUT: api/Categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Category updatedCategory)
        {
            var result = await _mongoService.Categories.ReplaceOneAsync(c => c.Id == id, updatedCategory);
            if (result.MatchedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/Categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mongoService.Categories.DeleteOneAsync(c => c.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
