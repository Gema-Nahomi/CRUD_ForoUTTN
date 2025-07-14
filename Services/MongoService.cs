using CRUD_ForoUTTN.Models;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace CRUD_ForoUTTN.Services
{
    public class MongoService
    {
        private readonly IMongoDatabase _database;

        public MongoService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDB"));
            _database = client.GetDatabase(config["Database"]);
        }

        public IMongoCollection<Actionn> Actions => _database.GetCollection<Actionn>("actions");
        public IMongoCollection<Admin> Admins => _database.GetCollection<Admin>("admins");
        public IMongoCollection<Category> Categories => _database.GetCollection<Category>("categories");
        public IMongoCollection<Login> Login => _database.GetCollection<Login>("login");
        public IMongoCollection<Notification> Notifications => _database.GetCollection<Notification>("notifications");
        public IMongoCollection<Post> Posts => _database.GetCollection<Post>("posts");
        public IMongoCollection<Report> Reports => _database.GetCollection<Report>("reports");
        public IMongoCollection<Response> Responses => _database.GetCollection<Response>("responses");
        public IMongoCollection<SignUp> SignUp => _database.GetCollection<SignUp>("signup");
        public IMongoCollection<Users> Users => _database.GetCollection<Users>("users");
    }
}
