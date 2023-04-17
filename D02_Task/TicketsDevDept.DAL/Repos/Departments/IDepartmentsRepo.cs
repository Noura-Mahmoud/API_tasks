using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDevDept.DAL
{
    public interface IDepartmentsRepo
    {
        Department? GetDeptDetailsWithTicketsAndDevCount(int id);
    }
}
