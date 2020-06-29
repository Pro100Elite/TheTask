using AutoMapper;
using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheTask.Models;

namespace TheTask.Config
{
    public class PLMapper : Profile
    {
        public PLMapper()
        {
            CreateMap<DeptBL, DeptPL>().ReverseMap();
            CreateMap<EmpBL, EmpPL>().ReverseMap();
            CreateMap<EmpBL, EmpCreatePL>().ReverseMap();
        }
    }
}