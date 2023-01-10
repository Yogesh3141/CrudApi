using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CrudApi.Models
{
    public class IdNameModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; }
    }
}
