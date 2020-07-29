using AutoMapper;
using BL.Interfaces;
using BL.Models;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class EmpService: IEmpService
    {
        private readonly IEmpRepository _repsitory;
        private readonly IMapper _mapper;

        public EmpService(IEmpRepository repository, IMapper mapper)
        {
            _repsitory = repository;
            _mapper = mapper;
        }

        public IEnumerable<EmpBL> GetAll()
        {
            var data = _repsitory.GetAll();
            var emps = _mapper.Map<IEnumerable<EmpBL>>(data);

            return emps;
        }

        public IEnumerable<EmpBL> GetHierarchy()
        {
            var data = _repsitory.GetHierarchy();
            var emps = _mapper.Map<IEnumerable<EmpBL>>(data);

            return emps;
        }

        public EmpBL GetEmp(decimal? empNo)
        {
            var data = _repsitory.GetEmp(empNo);
            var emp = _mapper.Map<EmpBL>(data);

            return emp;
        }

        public IEnumerable<EmpBL> GetEmpsHierarchy(decimal? MgrNo)
        {
            var data = _repsitory.GetEmpsHierarchy(MgrNo);
            var emps = _mapper.Map<IEnumerable<EmpBL>>(data);

            return emps;
        }

        public void Create(EmpBL empBl)
        {
            var emp = _mapper.Map<Emp>(empBl);

            _repsitory.Create(emp);
        }

        public void Delete(decimal? empNo)
        {
            _repsitory.Delete(empNo);
        }

        public void Edit(EmpBL empBl)
        {
            var emp = _mapper.Map<Emp>(empBl);

            _repsitory.Edit(emp);
        }
    }
}
