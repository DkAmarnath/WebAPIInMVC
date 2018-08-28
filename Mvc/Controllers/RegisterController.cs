using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc.Models;
using System.Net.Http;

namespace Mvc.Controllers
{
    public class RegisterController : Controller
    {
        public string str;
        // GET: Register
        //[HttpPost]
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Register()
        {
            IEnumerable<Country> _list;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Register/CountryDetails").Result;
            _list = response.Content.ReadAsAsync<IEnumerable<Country>>().Result;
            ViewBag.CountryList = new SelectList(_list, "CountryId", "CountryName");
            ModelState.Clear();
            return View();
        }
        [HttpPost]
        public ActionResult RegisterForm(MVCRegisterModel obj)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Register/UserDetailsInsert", obj).Result;

                return RedirectToAction("LoginPage", "Register");
            }
            return RedirectToAction("Index", "Register");
            
        }  
        //States dropdownlist based on Country dropdownlist
        public JsonResult GetStateList(int CountryId)
        {
            IEnumerable<State> _list;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync(string.Format("Register/statedetails/{0}", CountryId)).Result;
            if (response.IsSuccessStatusCode)
            {
            }
            _list = response.Content.ReadAsAsync<IEnumerable<State>>().Result;
            return Json(_list, JsonRequestBehavior.AllowGet);
            //return View();

        }
        //City dropdownlist based on States dropdownlist
        public JsonResult GetCityList(int StateId)
        {
            IEnumerable<City> _list;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync(string.Format("Register/citydetails/{0}", StateId)).Result;
            if (response.IsSuccessStatusCode)
            {
            }
            _list = response.Content.ReadAsAsync<IEnumerable<City>>().Result;
            return Json(_list, JsonRequestBehavior.AllowGet);
            //return View();

        }

        public ActionResult LoginPage()
        {
            //ModelState.Clear();
            return View();
        }
        [HttpPost]
        public ActionResult LoginForm(MVCLoginModel obj)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync(string.Format("Register/UserDetailsInsert/{0}/{1}",obj.UserName,obj.Password)).Result;
                TempData["Username"] = obj.UserName;
                return RedirectToAction("Welcome", "Register");
            }
            return RedirectToAction("LoginPage", "Register");

        }
        [HttpGet]
        public ActionResult Welcome(MVCLoginModel obj)
        {
            TempData["Username"] = TempData["Username"];
            return View();
            //if (ModelState.IsValid)
            //{
            //    HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Register/UserDetailsInsert", obj).Result;

            //    return RedirectToAction("Register", "Welcome");
            //}
            //return RedirectToAction("Register", "LoginPage");

        }
        //[HttpPost]
        //[AllowAnonymous]
        public JsonResult IsUserNameAvailable(string UserName)
        {
       HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync(string.Format("Register/IsUserNameExist/{0}", UserName)).Result;
          bool t = response.Content.ReadAsAsync<bool>().Result;
            //bool xx = BlobManager.IsNameAvailable(Name);
            //if (!xx)
            //{
            if(t==true)
                return Json("The name already exists", JsonRequestBehavior.AllowGet);
            else
                return Json(true, JsonRequestBehavior.AllowGet);
            //}
            //return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}