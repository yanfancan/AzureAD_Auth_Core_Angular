using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HSC.RTD.AVLAggregatorCore.Data;
using HSC.RTD.AVLAggregatorCore.Data.POCO;
using AVLAdminApp.WebServiceModels;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;

namespace AVLAdminApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsersController : ControllerBase
    {

        public const string Authenticated = "_authenticated";
        public const string Username = "username";

        private readonly IAvlRepository Repo;
        public UsersController(IAvlRepository repo)
        {
            Repo = repo;
        }

        // Login
        [HttpPost("login")]
        public IActionResult Post(PostUser user)
        {
            signin();
            //validate user
            User usr = Repo.GetUserByEmail(user.username);
            if (usr != null && usr.Password == user.password)
            {

                HttpContext.Session.SetInt32(Authenticated, 1);
                HttpContext.Session.SetString(Username, usr.FirstName);

                return Ok(new
                {
                    Id = 23,
                    usr.FirstName,
                    usr.LastName,
                    usr.Email
                });
            }

            //return new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Oops!!!" };
            return Unauthorized();
        }
        [HttpGet("signin")]
        public IActionResult signin()
        {
            var cr =  Challenge(
                new AuthenticationProperties { },
                OpenIdConnectDefaults.AuthenticationScheme);
            return Ok(User.Identity.IsAuthenticated);
        }


        // GET USERS
        [HttpGet("users")]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            IEnumerable<User> result = Repo.GetUsers();
            foreach (var user in result)
            {
                user.Password = null;
            }
            return new ActionResult<IEnumerable<User>>(result);

        }

        // UPDATE USERS
        [HttpPost("update-users")]
        //public IActionResult updateUsers(User[] users)
        public IActionResult updateUsers(IEnumerable<User> users)
        {
            Repo.updateUsers(users);
            return Ok();
        }

        // NEW USERS
        [HttpPost("new-users")]
        public IActionResult newUsers(User[] users)
        {
            Repo.newUsers(users);
            return Ok();
        }

        // UPDATE SERVICE ACCOUNTS
        [HttpPost("update-serviceAccounts")]
        //public IActionResult updateUsers(User[] users)
        public IActionResult updateServiceAccounts(IEnumerable<ServiceAccount> users)
        {
            Repo.updateServiceAccounts(users);
            return Ok();
        }

        // NEW SERVICE ACCOUNTS
        [HttpPost("new-serviceAccounts")]
        public IActionResult newServiceAccounts(ServiceAccount[] serviceAccounts)
        {
            Repo.newServiceAccounts(serviceAccounts);
            return Ok();
        }

        // DELETE USERS
        [HttpPost("delete-users")]
        public IActionResult deleteUsers(User[] users)
        {
            Repo.deleteUsers(users);
            return Ok();
        }

        // DELETE SERVICE ACCOUNTS
        [HttpPost("delete-serviceAccounts")]
        public IActionResult deleteServiceAccounts(ServiceAccount[] serviceAccounts)
        {
            Repo.deleteServiceAccounts(serviceAccounts);
            return Ok();
        }


        // GET SERVICE ACCOUNTS
        [HttpGet("serviceAccounts")]
        public ActionResult<IEnumerable<ServiceAccount>> GetServiceAccounts()
        {
            return new ActionResult<IEnumerable<ServiceAccount>>(Repo.GetServiceAccounts());
        }

        // GET SERVICES
        [HttpGet("services")]
        public ActionResult<IEnumerable<Service>> GetServices()
        {
            return new ActionResult<IEnumerable<Service>>(Repo.GetServices());
        }
        // GET SERVICES
        [HttpGet("associatedServices/{i}")]
        public ActionResult<IEnumerable<Service>> GetAssociatedServices(int i)
        {
            return new ActionResult<IEnumerable<Service>>(Repo.GetAssociatedServices(i));
        }

        // GET SERVICES
        [HttpGet("associatedServices2/{i}")]
        public int GetAssociatedServices2(int i)
        {
            return i;
        }
    }
}