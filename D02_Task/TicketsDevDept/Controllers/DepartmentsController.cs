using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketsDevDept.BLL;

namespace TicketsDevDept.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsManager departmentsManager;

        public DepartmentsController(IDepartmentsManager departmentsManager)
        {
            this.departmentsManager = departmentsManager;
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<DepartmentReadWithTicketsAndDevCountDto> GetDetailsById(int id) 
        {
            var dept = departmentsManager.GetDeptWithTicketsAndDevs(id);
            if (dept == null)
                return NotFound();
            else
                return dept;
        }
    }
}
