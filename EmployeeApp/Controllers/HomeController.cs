using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeApp.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Contact()
        {
            ViewBag.Message = "Made by Lukas Raila";

            return View();
        }
    }
}