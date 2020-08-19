using AutoMapper;
using BL.Models;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Config
{
    public class BLMapper : Profile
    {
        public BLMapper()
        {
            CreateMap<Dept, DeptBL>().ReverseMap();
            CreateMap<Emp, EmpBL>().ReverseMap();
            CreateMap<EmpPlusSalGrade, EmpPlusSalGradeBL>().ReverseMap();
            CreateMap<EmpPlusDName, EmpPlusDNameBL>().ReverseMap();
        }
    }
}
