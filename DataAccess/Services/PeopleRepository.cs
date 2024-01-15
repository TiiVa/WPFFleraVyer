using DataAccess.Enteties;
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
        var client = new MongoClient($"mongodb://{hostName}:{port}");
        var database = client.GetDatabase(databaseName);
        _people = database.GetCollection<Person>("People", new MongoCollectionSettings() { AssignIdOnInsert = true });
    }

    public string AddPerson(string firstName, string lastName)
    {
        var newPerson = new Person()
        {
            FirstName = firstName,
            LastName = lastName
        };

        _people.InsertOne(newPerson);

        var filter = Builders<Person>.Filter.Eq("FirstName", firstName);
        var personSaved = _people.Find(filter).FirstOrDefault();
        return personSaved.Id.ToString();
    }

    public List<Person> GetAllPeople()
    {
        var filter = Builders<Person>.Filter.Empty;
        var allPeople = _people.Find(filter).ToList();
        return allPeople;
    }

    public Person UpdateLastNameForPerson(string firstName, string newLastName)
    {
        var filter = Builders<Person>.Filter
            .Eq("FirstName", firstName);
        var update = Builders<Person>.Update
            .Set(person => person.LastName, newLastName);

        _people.UpdateOne(filter, update);

        return _people.Find(filter).FirstOrDefault();
    }

    public void DeletePerson(string firstName)
    {
        var filter = Builders<Person>.Filter.Eq("FirstName", firstName);
        _people.DeleteOne(filter);
    }
}