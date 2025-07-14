using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace CRUD_ForoUTTN.Models
{
    public class Report
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }  // Referencia al usuario que reporta

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? PostId { get; set; }  // Referencia al post que se reporta

        [BsonElement("description")]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [BsonElement("status")]
        [JsonPropertyName("status")]
        public string Status { get; set; }  // Ej. "pending", "resolved"

        [BsonElement("report_date")]
        [JsonPropertyName("report_date")]
        public DateTime ReportDate { get; set; }
    }
}
