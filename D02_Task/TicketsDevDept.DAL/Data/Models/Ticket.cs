﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDevDept.DAL
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
        public virtual ICollection<Developer> Developers { get; set; } = new HashSet<Developer>();
    }
}
