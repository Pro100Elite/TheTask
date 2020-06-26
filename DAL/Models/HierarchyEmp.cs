using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class HierarchyEmp
    {
        public Emp Emp { get; set; }

        public ICollection<Emp> Subordinates { get; set; }
    }
}
