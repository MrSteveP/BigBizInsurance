using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BigBizInsurance.Application.Services.Abstraction;
using UserManagement.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BigBizInsurance.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupsController : ControllerBase
    {
        public LookupsController()
        {
        }


        // GET api/<LookupsController>/5
        [HttpGet("Samples")]
        public JsonResult GetSamples()
        {

            //var options=  SamplesService.GetSelectListItems(gradeId.Value);
            //  return new JsonResult(new { Result = true, Options = options });
            return null;

        }

    }
}
