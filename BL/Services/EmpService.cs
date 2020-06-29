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
    }
}
