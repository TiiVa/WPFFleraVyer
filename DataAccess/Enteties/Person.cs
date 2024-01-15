using Common.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Enteties;

public class Person
{
    public ObjectId Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    [BsonRepresentation(BsonType.String)]
    public HairColor HairColor { get; set; }

    public List<Pet> Pets { get; set; } = 
        [
            new Pet() { Name = "Babe", AnimalType = AnimalType.Piglet },
            new Pet() { Name = "Zoe", AnimalType = AnimalType.Dog }
        ];
}