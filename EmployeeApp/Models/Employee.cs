using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeApp.Models
{
    [BsonIgnoreExtraElements]
    public class Employee
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("FirstName")]
        public string FirstName { get; set; } = string.Empty;
        [BsonElement("LastName")]
        public string LastName { get; set; } = string.Empty;
        [BsonElement("NetSalary")]
        public float NetSalary { get; set; } = 0f;
        [BsonIgnore]
        public float GrossSalary { get; set; }
    }
}