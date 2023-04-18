using IdentityApi.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly UserManager<Employee> userManager;

        public DataController(UserManager<Employee> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetData()
        {
            return Ok(new { Data = "Secured Data" });
        }

        [HttpGet]
        [Authorize(Policy = "AllowManagers")]
        [Route("manager")]
        public ActionResult GetDataForManager()
        {
            return Ok(new { Data = "Secured Data for managers" });
        }

        [HttpGet]
        [Authorize (Policy = "AllowEngineers")]
        [Route("engineer")]
        public ActionResult GetDataForEngineer()
        {
            return Ok(new { Data = "Secured Data for engineers" });
        }
    }
}
