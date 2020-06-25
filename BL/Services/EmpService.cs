﻿using AutoMapper;
using BL.Interfaces;
using BL.Models;
using DAL.Interfaces;
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

        public IEnumerable<EmpBL> GetEmpsHierarchy()
        {
            var data = _repsitory.GetEmpsHierarchy();
            var emps = _mapper.Map<IEnumerable<EmpBL>>(data);

            return emps;
        }
    }
}
