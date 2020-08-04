using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class DeptBL
    {
        public decimal DeptNo { get; set; }
        public string DeptName { get; set; }
        public string Loc { get; set; }
        public ICollection<EmpBL> _Emps { get; set; }
    }
}
