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
    public class PostsController : ControllerBase
    {
        private readonly MongoService _mongoService;

        public PostsController(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _mongoService.Posts.Find(Builders<Post>.Filter.Empty).ToListAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var post = await _mongoService.Posts.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Post newPost)
        {
            if (newPost == null)
            {
                return BadRequest("Post data cannot be null");
            }

            await _mongoService.Posts.InsertOneAsync(newPost);
            return CreatedAtAction(nameof(GetById), new { id = newPost.Id }, newPost);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Post updatedPost)
        {
            if (updatedPost == null)
            {
                return BadRequest("Post data cannot be null");
            }

            var result = await _mongoService.Posts.ReplaceOneAsync(p => p.Id == id, updatedPost);
            if (result.MatchedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mongoService.Posts.DeleteOneAsync(p => p.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
