using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace CRUD_ForoUTTN.Models
{
    public class Admin
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("user_id")]
        [JsonPropertyName("user:id")]
        public ObjectId UserId { get; set; }  // Referencia al usuario

        [BsonElement("assignment_date")]
        [JsonPropertyName("assignment_date")]
        public DateTime AssignmentDate { get; set; }

        [BsonElement("action_history")]
        [JsonPropertyName("action_history")]
        public List<ActionHistory> ActionHistory { get; set; }

        [BsonElement("permissions")]
        [JsonPropertyName("permissions")]
        public List<string> Permissions { get; set; }
    }

    public class ActionHistory
    {
        [BsonElement("action")]
        [JsonPropertyName("action")]
        public string Action { get; set; }

        [BsonElement("date")]
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
    }
}
