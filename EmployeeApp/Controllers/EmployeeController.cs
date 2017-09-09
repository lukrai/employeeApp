using EmployeeApp.App_Start;
using EmployeeApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace EmployeeApp.Controllers
{
    public class EmployeeController : Controller
    {
        MongoContext _dbContext;
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeController()
        {
            _dbContext = new MongoContext();
            _employeeRepository = new EmployeeRepository();

        }

        // GET: Employee
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";
            ViewBag.LastNameSortParm = sortOrder == "lastname" ? "lastname_desc" : "lastname";
            ViewBag.SalarySortParm = sortOrder == "netsalary" ? "netsalary_desc" : "netsalary";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var employees = from s in _employeeRepository.GetAllEmployees()
                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "firstname_desc":
                    employees = employees.OrderByDescending(s => s.FirstName);
                    break;
                case "lastname":
                    employees = employees.OrderBy(s => s.LastName);
                    break;
                case "lastname_desc":
                    employees = employees.OrderByDescending(s => s.LastName);
                    break;
                case "netsalary":
                    employees = employees.OrderBy(s => s.NetSalary);
                    break;
                case "netsalary_desc":
                    employees = employees.OrderByDescending(s => s.NetSalary);
                    break;
                default:
                    employees = employees.OrderBy(s => s.FirstName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(employees.ToPagedList(pageNumber, pageSize));

        }

        
        // GET: Employee/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                var document = _employeeRepository.GetEmployee(id);
                return View(document);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
        
            try
            {
                _employeeRepository.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(string id)
        {       
            try
            {
                var document = _employeeRepository.GetEmployee(id);              
                return View(document);               
            }
            catch
            {
                return RedirectToAction("Index");
            }          
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Employee employee)
        {
            try
            {
                employee.Id = new ObjectId(id);
                // TODO: Add update logic here
                _employeeRepository.UpdateEmployee(employee.Id, employee);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                var document = _employeeRepository.GetEmployee(id);
                return View(document);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Employee employee)
        {
            try
            {
                _employeeRepository.RemoveEmployee(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
