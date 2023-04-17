using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDevDept.DAL
{
    public class DevelopersRepo : IDevelopersRepo
    {
        private readonly Context context;

        public DevelopersRepo(Context context)
        {
            this.context = context;
        }
        public IEnumerable<Developer> GetByDevsIds(int[] ids)
        {
            return context.Set<Developer>()
                .Where(d => ids.Contains(d.Id));
        }
    }
}
