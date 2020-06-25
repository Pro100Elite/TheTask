using AutoMapper;
using BL.Interfaces;
using BL.Models;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class DeptService : IDeptService
    {
        private readonly IDeptRepository _repsitory;
        private readonly IMapper _mapper;

        public DeptService(IDeptRepository repository, IMapper mapper)
        {
            _repsitory = repository;
            _mapper = mapper;
        }

        public IEnumerable<DeptBL> GetAll()
        {
            var data = _repsitory.GetAll();
            var depts = _mapper.Map<IEnumerable<DeptBL>>(data);
            
            return depts;
        }
    }
}
