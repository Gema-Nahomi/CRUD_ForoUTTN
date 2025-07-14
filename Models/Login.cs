using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace CRUD_ForoUTTN.Models
{
    public class Login
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("email")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [BsonElement("contraseña")]
        [JsonPropertyName("contraseña")]
        public string Contraseña { get; set; }

        [BsonElement("fecha_inicio_sesion")]
        [JsonPropertyName("fecha_inicio_sesion")]
        public DateTime FechaInicioSesion { get; set; }

        [BsonElement("token")]
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
