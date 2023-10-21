using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class CampaignGuide
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonIgnoreIfNull]
        public string? Code { get; set; }

        [BsonIgnoreIfNull]
        public string? CampaignName { get; set; }

        [BsonIgnoreIfNull]
        public string? Summary { get; set; }

        [BsonIgnoreIfNull]
        public string? StarDate { get; set; }

        [BsonIgnoreIfNull]
        public string? EndingDate { get; set; }

        [BsonIgnoreIfNull]
        public String? CampaignInformation { get; set; }

        [BsonIgnoreIfNull]
        public String? CallScript { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public String? LastEdition { get; set; }
    }
}
