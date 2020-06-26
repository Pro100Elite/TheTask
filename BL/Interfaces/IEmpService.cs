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
        IEnumerable<EmpBL> GetAll();
        IEnumerable<EmpBL> GetEmpsHierarchy(decimal? MgrNo);
    }
}
