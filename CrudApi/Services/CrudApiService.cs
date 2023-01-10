using CrudApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CrudApi.Services
{
    public class CrudApiService
    {
        private readonly IMongoCollection<CrudApiModel>Employeecollection;
      
     
        public CrudApiService(IOptions<CrudApiDatabaseSettings>crudApiDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                crudApiDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                crudApiDatabaseSettings.Value.DatabaseName);

            Employeecollection = mongoDatabase.GetCollection<CrudApiModel>(
                crudApiDatabaseSettings.Value.CollectionName);

        }

        public async Task<List<CrudApiModel>> GetAsync()
        //await Employeecollection.Find(_ => true).ToListAsync();
        {
            return await Employeecollection.Aggregate()
                .Lookup("Empsalary", "Role", "Role", "data")
                .Unwind<CrudApiModel>("data")
                .Lookup("Teams", "Team", "_id", "Team")
                .Unwind<CrudApiModel>("Team")
                .Project<CrudApiModel>(new BsonDocument
                {
                    {"EmployeeName",1},
                    {"Email",1},
                    {"BankName",1},
                    {"BankAddress",1},
                    {"Photo",1},
                    {"Signature",1 },
                    {"salary","$data.salary"},
                    {"Team","$Team.Name" }

                }).ToListAsync();
        }

        public async Task<CrudApiModel?> GetAsync(string id) =>
            await Employeecollection.Find((x) => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(CrudApiModel newInsert) =>
            await Employeecollection.InsertOneAsync(newInsert);

        public async Task UpdateAsync(string id, CrudApiModel updatedItem) =>
            await Employeecollection.ReplaceOneAsync(x => x.Id == id, updatedItem);

        public async Task RemoveAsync(string id) =>
            await Employeecollection.DeleteOneAsync(x => x.Id == id);


    }

    public class TeamService
    {

        private readonly IMongoCollection<IdNameModel> teamCollection;
        public TeamService(IOptions<CrudApiDatabaseSettings> crudApiDatabaseSettings)

        {
            var mongoClient = new MongoClient(
                crudApiDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                crudApiDatabaseSettings.Value.DatabaseName);

            teamCollection = mongoDatabase.GetCollection<IdNameModel>(
                crudApiDatabaseSettings.Value.CollectionTeam);

        }
        public async Task<List<IdNameModel>> GetKAsync() =>
            await teamCollection.Find(_ => true).ToListAsync();

    }
}
