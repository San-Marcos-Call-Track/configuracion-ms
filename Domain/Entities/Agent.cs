using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

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
    }
}
