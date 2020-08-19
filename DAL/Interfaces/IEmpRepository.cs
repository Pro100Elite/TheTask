using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IEmpRepository
    {
        Emp GetEmp(decimal? empNo);
        IEnumerable<Emp> GetAll();
        IEnumerable<EmpPlusDName> GetAllPlusDName();
        IEnumerable<Emp> GetByDept(decimal deptNo);
        IEnumerable<Emp> GetHierarchy();
        IEnumerable<EmpPlusSalGrade> GetEmpsHierarchy(decimal? MgrNo);
        void Create(Emp emp);
        void Delete(decimal? empNo);
        void Edit(Emp emp);
    }
}
