using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace CRUD_ForoUTTN.Models
{
    public class Response
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UsuarioId { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? PreguntaId { get; set; }  // Referencia a la pregunta (Post)

        [BsonElement("contenido")]
        [JsonPropertyName("contenido")]
        public string Contenido { get; set; }

        [BsonElement("fecha_respuesta")]
        [JsonPropertyName("fecha_respuesta")]
        public DateTime FechaRespuesta { get; set; }
    }
}
