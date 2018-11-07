using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HSC.RTD.AVLAggregatorCore.Data;
using HSC.RTD.AVLAggregatorCore.Data.POCO;
using AVLAdminApp.WebServiceModels;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace AVLAdminApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DevicesController : ControllerBase
    {
        private readonly IAvlRepository Repo;
        public DevicesController(IAvlRepository repo)
        {
            Repo = repo;
        }

        // GET DEVICES
        [HttpGet("devices")]
        public ActionResult<IEnumerable<Device>> GetUsers()
        {
            IEnumerable<Device> result = Repo.GetDevices();
            return new ActionResult<IEnumerable<Device>>(result);

        }

        // UPDATE DEVICES
        [HttpPost("update-devices")]
        public IActionResult updateServiceAccounts(IEnumerable<Device> devices)
        {
            Repo.updateDevices(devices);
            return Ok();
        }

        // NEW DEVICES
        [HttpPost("new-devices")]
        public IActionResult newServiceAccounts(Device[] devices)
        {
            Repo.newDevices(devices);
            return Ok();
        }

        // DELETE DEVICES
        [HttpPost("delete-devices")]
        public IActionResult deleteUsers(Device[] devices)
        {
            Repo.deleteDevices(devices);
            return Ok();
        }

    }
}