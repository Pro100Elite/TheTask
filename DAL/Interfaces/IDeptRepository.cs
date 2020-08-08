using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDeptRepository
    {
        IEnumerable<Dept> GetAll();
        Dept GetDept(decimal deptNo);
        void Create(Dept dept);
        void Edit(Dept dept);
        void Delete(decimal deptNo);
    }
}
