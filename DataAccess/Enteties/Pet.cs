using Common.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Enteties;

public class Pet
{
    public string Name { get; set; }

    [BsonRepresentation(BsonType.String)]
    public AnimalType AnimalType { get; set; }
}