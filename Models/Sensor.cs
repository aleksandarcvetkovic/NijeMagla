using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Nije_Magla_API
{
    [BsonIgnoreExtraElements]
    public class Sensor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("ime")]
        public string Ime { get; set; } = String.Empty;


        [BsonElement("lokacija")]
        public string Lokacija { get; set; }

        [BsonElement("Lat")]
        public double lat { get; set; }

        [BsonElement("Lng")]
        public double lng { get; set; }
    }
}
