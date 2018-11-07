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
using Microsoft.AspNetCore.Authorization;

namespace HSC.RTD.AVLAdminApp.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {

        private readonly IAvlRepository Repo;
        public ValuesController(IAvlRepository repo)
        {
            Repo = repo;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Position>> Get()
        {
            return new ActionResult<IEnumerable<Position>>(Repo.GetChangedPositionsBySessionId(6,30));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        


    }
}
