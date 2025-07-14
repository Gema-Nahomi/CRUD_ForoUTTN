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
    public class ReportsController : ControllerBase
    {
        private readonly MongoService _mongoService;

        public ReportsController(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reports = await _mongoService.Reports.Find(Builders<Report>.Filter.Empty).ToListAsync();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var report = await _mongoService.Reports.Find(r => r.Id == id).FirstOrDefaultAsync();
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Report newReport)
        {
            if (newReport == null)
            {
                return BadRequest("Report data cannot be null");
            }

            await _mongoService.Reports.InsertOneAsync(newReport);
            return CreatedAtAction(nameof(GetById), new { id = newReport.Id }, newReport);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Report updatedReport)
        {
            if (updatedReport == null)
            {
                return BadRequest("Report data cannot be null");
            }

            var result = await _mongoService.Reports.ReplaceOneAsync(r => r.Id == id, updatedReport);
            if (result.MatchedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mongoService.Reports.DeleteOneAsync(r => r.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
