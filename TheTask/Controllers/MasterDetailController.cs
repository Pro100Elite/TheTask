using AutoMapper;
using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheTask.Models;

namespace TheTask.Controllers
{
    public class MasterDetailController : Controller
    {
        private readonly IDeptService _deptService;
        private readonly IEmpService _empService;
        private readonly IMapper _mapper;

        public MasterDetailController(IDeptService deptService, IEmpService empService, IMapper mapper)
        {
            _deptService = deptService;
            _empService = empService;
            _mapper = mapper;
        }

        public ActionResult MasterDet()
        {
            var data = _deptService.GetAll();
            var depts = _mapper.Map<IEnumerable<DeptPL>>(data);

            return View(depts);
        }

        public ActionResult DetailData(decimal dept)
        {
            var data = _empService.GetByDept(dept);
            var emps = _mapper.Map<IEnumerable<EmpPL>>(data);
            ViewBag.d = dept;
            return PartialView("_Emps", emps);
        }
    }
}