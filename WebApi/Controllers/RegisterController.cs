using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("api/Register")] 
    public class RegisterController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Register/CountryDetails
        [HttpGet]
        [Route("CountryDetails")]
        [ResponseType(typeof(Country))]
        public IHttpActionResult BindCountryDetails()
        {
            this.db.Configuration.ProxyCreationEnabled = false;
            List<Country> Detail = new List<Country>();
            try
            {
                 Detail = db.Countries.ToList();
            }
            catch(ApplicationException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = ex.Message });
            }
            catch(Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadGateway, ReasonPhrase = ex.Message });
            }
            return Ok(Detail != null ? Detail : null);
        }
        // GET: api/Register/CountryDetails
        [HttpGet]
        [Route("StateDetails/{countryid}")]
        [ResponseType(typeof(State))]
        public IHttpActionResult BindStateDetails(int CountryId)
        {
            this.db.Configuration.ProxyCreationEnabled = false;
            List<State> Detail = new List<State>();
            try
            {
                Detail = db.States.Where(a=>a.CountryId==CountryId).ToList();
            }
            catch (ApplicationException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = ex.Message });
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadGateway, ReasonPhrase = ex.Message });
            }
            return Ok(Detail != null ? Detail : null);
        }
        // GET: api/Register/CityDetails
        [HttpGet]
        [Route("CityDetails/{stateid}")]
        [ResponseType(typeof(City))]
        public IHttpActionResult BindCityDetails(int stateId)
        {
            this.db.Configuration.ProxyCreationEnabled = false;
            List<City> Detail = new List<City>();
            try
            {
                Detail = db.Cities.Where(a=>a.StateId==stateId).ToList();
            }
            catch (ApplicationException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = ex.Message });
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadGateway, ReasonPhrase = ex.Message });
            }
            return Ok(Detail != null ? Detail : null);
        }
        [HttpPost]
        //POST:api/Register/class
        [Route("UserDetailsInsert")]
        //[ResponseType(typeof(UserDetail))]
        public bool UserDetailsInsert([FromBody]UserDetail p)
        {
            this.db.Configuration.ProxyCreationEnabled = false;
            db.UserDetails.Add(p);
            db.SaveChanges();
            HttpResponseMessage httpmsg = new HttpResponseMessage();
            //return Ok(HttpStatusCode.OK);
            return httpmsg.IsSuccessStatusCode;
        }

        [HttpGet]
        //POST:api/Register/class
        [Route("LoginUserDetailsCheck/{UserName}/{Password}")]
        //[ResponseType(typeof(UserDetail))]
        public bool LoginUserDetailsCheck(string UserName,string Password)
        {
            this.db.Configuration.ProxyCreationEnabled = false;
            List<UserDetail> _lst = new List<UserDetail>();
            _lst = db.UserDetails.Where(m => m.UserName == UserName).Where(m => m.Password == Password).ToList();
           // db.SaveChanges();
            if (_lst.Count >= 1)
            {
                return true;
            }
            //HttpResponseMessage httpmsg = new HttpResponseMessage();
            //return Ok(HttpStatusCode.OK);
            else {
                return false;
            } 
        }
        [HttpGet]
        //GET:api/Register/IsUsernameExist
        [Route("IsUserNameExist/{UserName}")]
        public bool IsUserNameExist(string UserName)
        {
            this.db.Configuration.ProxyCreationEnabled = false;
            List<UserDetail> _lst = new List<UserDetail>();
            _lst = db.UserDetails.Where(m => m.UserName == UserName).ToList();
            if (_lst.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
