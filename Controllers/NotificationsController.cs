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
    public class NotificationsController : ControllerBase
    {
        private readonly MongoService _mongoService;

        public NotificationsController(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notifications = await _mongoService.Notifications.Find(Builders<Notification>.Filter.Empty).ToListAsync();
            return Ok(notifications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var notification = await _mongoService.Notifications.Find(n => n.Id == id).FirstOrDefaultAsync();
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Notification newNotification)
        {
            if (newNotification == null)
            {
                return BadRequest("Notification data cannot be null");
            }

            await _mongoService.Notifications.InsertOneAsync(newNotification);
            return CreatedAtAction(nameof(GetById), new { id = newNotification.Id }, newNotification);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Notification updatedNotification)
        {
            if (updatedNotification == null)
            {
                return BadRequest("Notification data cannot be null");
            }

            var result = await _mongoService.Notifications.ReplaceOneAsync(n => n.Id == id, updatedNotification);
            if (result.MatchedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mongoService.Notifications.DeleteOneAsync(n => n.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
