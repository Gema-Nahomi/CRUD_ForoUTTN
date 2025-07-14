using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace CRUD_ForoUTTN.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UsuarioId { get; set; }  // Referencia al usuario

        [BsonElement("titulo")]
        [JsonPropertyName("titulo")]
        public string Titulo { get; set; }

        [BsonElement("contenido")]
        [JsonPropertyName("contenido")]
        public string Contenido { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CategoriaId { get; set; } // Referencia a la categoría

        [BsonElement("fecha_publicacion")]
        [JsonPropertyName("fecha_publicacion")]
        public DateTime FechaPublicacion { get; set; }
    }
}
