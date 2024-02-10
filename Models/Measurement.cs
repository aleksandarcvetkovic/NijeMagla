using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Nije_Magla_API
{
    [BsonIgnoreExtraElements]
    public class Measurement
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("value")]
        public int Value { get; set; } = -1;

        [BsonElement("time")]
        public DateTime Time { get; set; }

        [BsonElement("idSenzora")]
        public string IdSenzora { get; set; } = String.Empty;
    }
}
