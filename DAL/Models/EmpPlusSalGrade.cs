using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class EmpPlusSalGrade
    {
        public decimal EmpNo { get; set; }
        public string EmpName { get; set; }
        public string Job { get; set; }
        public decimal? Mgr { get; set; }
        public DateTime? HireDate { get; set; }
        public decimal? Sal { get; set; }
        public decimal? Comm { get; set; }
        public decimal? DeptNo { get; set; }
        public int Grade { get; set; }

        private EntityRef<Dept> _Dept;
        [Association(Storage = "_Dept", ThisKey = "DeptNo")]
        public Dept Dept
        {
            get { return this._Dept.Entity; }
            set { this._Dept.Entity = value; }
        }
    }
}
