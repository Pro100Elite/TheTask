using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IDeptService
    {
        IEnumerable<DeptBL> GetAll();
        DeptBL GetDept(decimal deptNo);
        void Create(DeptBL deptBl);
        void Edit(DeptBL deptBl);
        void Delete(decimal deptNo);
    }
}
