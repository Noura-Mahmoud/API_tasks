using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDevDept.DAL
{
    public interface IDevelopersRepo
    {
        IEnumerable<Developer> GetByDevsIds(int[] ids);
    }
}
