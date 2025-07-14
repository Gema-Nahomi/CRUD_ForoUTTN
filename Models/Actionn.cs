using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace CRUD_ForoUTTN.Models
{
    public class Actionn
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }  // Referencia al usuario (administrador)

        [BsonElement("action_type")]
        [JsonPropertyName("action_type")]
        public string ActionType { get; set; }  // Tipo de acción (ej. "Modified question", "Deleted post")

        [BsonElement("details")]
        [JsonPropertyName("details")]
        public string Details { get; set; }  // Detalles de la acción realizada

        [BsonElement("action_date")]
        [JsonPropertyName("action_date")]
        public DateTime ActionDate { get; set; }  // Fecha y hora en la que se realizó la acción
    }
}
