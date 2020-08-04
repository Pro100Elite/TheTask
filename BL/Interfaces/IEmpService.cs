using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IEmpService
    {
        EmpBL GetEmp(decimal? empNo);
        IEnumerable<EmpBL> GetAll();
        IEnumerable<EmpBL> GetHierarchy();
        IEnumerable<EmpPlusSalGradeBL> GetEmpsHierarchy(decimal? MgrNo);
        void Create(EmpBL emp);
        void Delete(decimal? empNo);
        void Edit(EmpBL empBl);
    }
}
