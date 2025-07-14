using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace CRUD_ForoUTTN.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nombre")]
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [BsonElement("descripcion")]
        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        [BsonElement("preguntas")]
        [JsonPropertyName("preguntas")]
        public List<ObjectId> Preguntas { get; set; }  // Lista de preguntas asociadas

        [BsonElement("FAQ")]
        [JsonPropertyName("FAQ")]
        public bool FAQ { get; set; }  // Si es una categoría de preguntas frecuentes
    }
}
