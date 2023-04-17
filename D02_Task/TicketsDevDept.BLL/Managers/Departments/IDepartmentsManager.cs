using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDevDept.BLL
{
    public interface IDepartmentsManager
    {
        DepartmentReadWithTicketsAndDevCountDto?  GetDeptWithTicketsAndDevs(int id);
    }
}
