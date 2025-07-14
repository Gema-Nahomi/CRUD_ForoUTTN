using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace CRUD_ForoUTTN.Models
{
    public class SignUp
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nombre_completo")]
        [JsonPropertyName("nombre_completo")]
        public string NombreCompleto { get; set; } = string.Empty;

        [BsonElement("email")]
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("contraseña")]
        [JsonPropertyName("contraseña")]
        public string Contraseña { get; set; } = string.Empty;

        [BsonElement("confirmacion_contraseña")]
        [JsonPropertyName("confirmacion_contraseña")]
        public string ConfirmacionContraseña { get; set; } = string.Empty;
    }
}
