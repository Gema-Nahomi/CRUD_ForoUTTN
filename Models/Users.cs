using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CRUD_ForoUTTN.Models
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nombre")]
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [BsonElement("email")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [BsonElement("contraseña")]
        [JsonPropertyName("contraseña")]
        public string Contraseña { get; set; }

        [BsonElement("rol")]
        [JsonPropertyName("rol")]
        public string Rol { get; set; }

        [BsonElement("fecha_registro")]
        [JsonPropertyName("fecha_registro")]
        public DateTime FechaRegistro { get; set; }

        [BsonElement("perfil")]
        [JsonPropertyName("perfil")]
        public Perfil Perfil { get; set; }
    }
    public class Perfil
    {
        [BsonElement("biografia")]
        [JsonPropertyName("biografia")]
        public string Biografia { get; set; }

        [BsonElement("foto_perfil")]
        [JsonPropertyName("foto_perfil")]
        public string FotoPerfil { get; set; }
    
         // Referencias a las preguntas publicadas y respuestas dadas
        [BsonRepresentation(BsonType.Array)]
        public List<ObjectId>? PreguntasPublicadas { get; set; }

        [BsonRepresentation(BsonType.Array)]
        public List<ObjectId>? RespuestasDadas { get; set; }

    }
}
