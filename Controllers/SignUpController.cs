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
    public class SignUpController : ControllerBase
    {
        private readonly MongoService _mongoService;

        public SignUpController(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SignUp>>> Get()
        {
            try
            {
                var signups = await _mongoService.SignUp.Find(Builders<SignUp>.Filter.Empty).ToListAsync();
                return Ok(signups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SignUp>> Get(string id)
        {
            try
            {
                var signup = await _mongoService.SignUp.Find(s => s.Id == id).FirstOrDefaultAsync();
                if (signup == null)
                {
                    return NotFound($"No se encontró el registro con ID: {id}");
                }
                return Ok(signup);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SignUp>> Create([FromBody] SignUp signup)
        {
            try
            {
                if (signup == null)
                {
                    return BadRequest("Los datos del signup no pueden ser nulos");
                }

                // Validación
                var errors = new List<string>();
                if (string.IsNullOrWhiteSpace(signup.NombreCompleto))
                    errors.Add("El nombre completo es requerido");

                if (string.IsNullOrWhiteSpace(signup.Email))
                    errors.Add("El email es requerido");
                else if (!signup.Email.EndsWith("@uttn.mx"))
                    errors.Add("El email debe terminar con @uttn.mx");

                if (string.IsNullOrWhiteSpace(signup.Contraseña))
                    errors.Add("La contraseña es requerida");

                if (string.IsNullOrWhiteSpace(signup.ConfirmacionContraseña))
                    errors.Add("La confirmación de contraseña es requerida");

                if (!string.IsNullOrWhiteSpace(signup.Contraseña) &&
                    !string.IsNullOrWhiteSpace(signup.ConfirmacionContraseña) &&
                    signup.Contraseña != signup.ConfirmacionContraseña)
                    errors.Add("Las contraseñas no coinciden");

                if (errors.Any())
                {
                    return BadRequest(new { errors = errors });
                }

                // Limpiar el ID para que MongoDB lo genere automáticamente
                signup.Id = null;

                // Insertar en la colección de MongoDB
                await _mongoService.SignUp.InsertOneAsync(signup);
                return CreatedAtAction(nameof(Get), new { id = signup.Id }, signup);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] SignUp signup)
        {
            try
            {
                if (signup == null)
                {
                    return BadRequest("Los datos del signup no pueden ser nulos");
                }

                var existingSignup = await _mongoService.SignUp.Find(s => s.Id == id).FirstOrDefaultAsync();
                if (existingSignup == null)
                {
                    return NotFound($"No se encontró el registro con ID: {id}");
                }

                signup.Id = id;
                await _mongoService.SignUp.ReplaceOneAsync(s => s.Id == id, signup);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var existingSignup = await _mongoService.SignUp.Find(s => s.Id == id).FirstOrDefaultAsync();
                if (existingSignup == null)
                {
                    return NotFound($"No se encontró el registro con ID: {id}");
                }

                await _mongoService.SignUp.DeleteOneAsync(s => s.Id == id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("test-connection")]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                var result = await _mongoService.SignUp.Find(Builders<SignUp>.Filter.Empty).ToListAsync();
                return Ok(new
                {
                    message = "Conexión exitosa a MongoDB",
                    documentCount = result.Count,
                    timestamp = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error de conexión a MongoDB",
                    error = ex.Message,
                    timestamp = DateTime.Now
                });
            }
        }

        // Endpoint de depuración para ver qué datos llegan
        [HttpPost("debug")]
        public ActionResult Debug([FromBody] object data)
        {
            return Ok(new
            {
                message = "Datos recibidos correctamente",
                data = data,
                timestamp = DateTime.Now
            });
        }
    }
}
