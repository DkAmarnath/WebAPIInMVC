using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc.Models;
using System.Net.Http;

namespace Mvc.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<MvcEmployeeModel> empList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("employee").Result;
            empList = response.Content.ReadAsAsync<IEnumerable<MvcEmployeeModel>>().Result;
            return View(empList);
        }
    }
}