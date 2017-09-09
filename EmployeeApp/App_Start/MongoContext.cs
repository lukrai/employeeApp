using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using System.Configuration;
using EmployeeApp.Models;

namespace EmployeeApp.App_Start
{
    public class MongoContext {

        MongoClient _client;
        public IMongoDatabase _database;

        public MongoContext()  
        {
            var MongoDatabaseName = ConfigurationManager.AppSettings["MongoDatabaseName"];
            //var MongoDatabaseName = "employee-app";

            var MongoConnectionString = ConfigurationManager.AppSettings["MongoConnectionString"];
            //var MongoConnectionString = "mongodb://lukrai:employee987@ds052649.mlab.com:52649/employee-app";

            _client = new MongoClient(MongoConnectionString);
            if (_client != null)
                _database = _client.GetDatabase(MongoDatabaseName);
           
        }

        public IMongoCollection<Employee> Employees
        {
            get
            {
                return _database.GetCollection<Employee>("Employee");
            }
        }
    }
}