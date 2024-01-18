using Common.DTOs;
using DataAccess.Enteties;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Services;

public class PeopleRepository
{
    private readonly IMongoCollection<Person> _people;

    public PeopleRepository()
    {
        var hostName = "localhost";
        var port = "27017";
        var databaseName = "PeopleDb";
        var connectionString = "mongodb+srv://testNiklas:apa123@cluster0.nviqhpj.mongodb.net/";//$"mongodb://{hostName}:{port}"
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _people = database.GetCollection<Person>("People", new MongoCollectionSettings() { AssignIdOnInsert = true });
    }

    public void AddPerson(PersonRecord personRecord)
    {
        var newPerson = new Person()
        {
            FirstName = personRecord.FirstName,
            LastName = personRecord.LastName
        };

        _people.InsertOne(newPerson);
    }

    public IEnumerable<PersonRecord> GetAllPeople()
    {
        var filter = Builders<Person>.Filter.Empty;
        var allPeople =
            _people.Find(filter).ToList()
                .Select(
                    p =>
                        new PersonRecord(p.Id.ToString(), p.FirstName, p.LastName)
                    );
        return allPeople;
    }

    public PersonRecord UpdateLastNameForPerson(string id, string newLastName)
    {
        var filter = Builders<Person>.Filter
            .Eq("_id",ObjectId.Parse(id));
        var update = Builders<Person>.Update
            .Set(person => person.LastName, newLastName);

        _people.UpdateOne(filter, update);
        var updatedPerson = _people.Find(filter).FirstOrDefault();

        return new PersonRecord(updatedPerson.Id.ToString(), updatedPerson.FirstName, updatedPerson.LastName);
    }

    public void DeletePerson(string id)
    {
        var filter = Builders<Person>.Filter.Eq("_id", ObjectId.Parse(id));
        _people.DeleteOne(filter);
    }
}