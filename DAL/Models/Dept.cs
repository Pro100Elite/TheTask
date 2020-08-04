using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Dept
    {
        public decimal DeptNo { get; set; }
        public string DeptName { get; set; }
        public string Loc { get; set; }

        private EntitySet<Emp> _Emps;
        [Association(Storage = "_Emps", OtherKey = "DeptNo")]
        public EntitySet<Emp> Emps
        {
            get { return this._Emps; }
            set { this._Emps.Assign(value); }
        }
    }
}
