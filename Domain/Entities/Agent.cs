using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Agent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonIgnoreIfNull]
        public string? Dni { get; set; }

        [BsonIgnoreIfNull]
        public string? FirstName { get; set; }

        [BsonIgnoreIfNull]
        public string? LastName { get; set; }

        [BsonIgnoreIfNull]
        public string? WorkGroup { get; set; }

        [BsonIgnoreIfNull]
        public string? PersonalMail { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public String? LastEdition { get; set; }
    }
}
