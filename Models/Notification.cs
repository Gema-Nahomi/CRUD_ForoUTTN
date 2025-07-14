using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace CRUD_ForoUTTN.Models
{
    public class Notification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UsuarioId { get; set; }  // Referencia al usuario que recibe la notificación

        [BsonElement("mensaje")]
        [JsonPropertyName("mensaje")]
        public string Message { get; set; }

        [BsonElement("fecha_notificacion")]
        [JsonPropertyName("fecha_notificacion")]
        public DateTime NotificationDate { get; set; }

        [BsonElement("tipo")]
        [JsonPropertyName("tipo")]
        public string Type { get; set; }  // Ej. "respuesta", "nuevo_post"

        [BsonElement("leida")]
        [JsonPropertyName("leida")]
        public bool Read { get; set; }  // Estado de la notificación (leída o no)
    }
}
