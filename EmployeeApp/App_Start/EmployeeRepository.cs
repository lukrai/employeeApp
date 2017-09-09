using EmployeeApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeApp.App_Start
{
    public class EmployeeRepository
    {
        private readonly MongoContext _context = null;

        public EmployeeRepository()
        {
            _context = new MongoContext();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            var documents =  _context.Employees.Find(_ => true).ToList();
            foreach (Employee employee in documents)
            {
                float gross = employee.NetSalary * 100 / 76;
                gross = (float)Math.Round(gross, 2);
                
                employee.GrossSalary = gross;
            }
            return documents;
        }

        public Employee GetEmployee(string id)
        {       
            var filter = Builders<Employee>.Filter.Eq(p => p.Id, new ObjectId(id));
            var document = _context.Employees.Find(filter).FirstOrDefault();

            return document;
        }

        public void AddEmployee(Employee item)
        {
             _context.Employees.InsertOne(item);
        }

        public bool RemoveEmployee(string id)
        {
            var result = _context.Employees.DeleteOne(
                 Builders<Employee>.Filter.Eq(p => p.Id, new ObjectId(id)));

            return result.DeletedCount > 0;
        }

        public void UpdateEmployee(ObjectId id, Employee item)
        {
            var filter = Builders<Employee>.Filter.Eq(s => s.Id, id);
            var result = _context.Employees.ReplaceOne(filter, item);
        }

        public void RemoveAllEmployees()
        {
            _context.Employees.DeleteManyAsync(new BsonDocument());
        }

    }

}