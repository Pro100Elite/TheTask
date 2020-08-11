using AutoMapper;
using BL.Interfaces;
using BL.Models;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheTask.Models;

namespace TheTask.Controllers
{
    public class EmpController : Controller
    {

        private readonly IEmpService _service;
        private readonly IDeptService _deptService;
        private readonly IMapper _mapper;

        public EmpController(IEmpService service, IDeptService deptService, IMapper mapper)
        {
            _service = service;
            _deptService = deptService;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            var data = _service.GetAll();
            var emps = _mapper.Map<IEnumerable<EmpPL>>(data);

            return View(emps);
        }

        public ActionResult GetHierarchyAvgDept()
        {
            var data = _service.GetHierarchy();
            var emps = _mapper.Map<IEnumerable<EmpPL>>(data).GroupBy(x => x.DeptNo);

            return View(emps);
        }

        public ActionResult GetSubordinates(decimal? MgrNo)
        {
            var data = _service.GetEmpsHierarchy(MgrNo);
            var emps = _mapper.Map<IEnumerable<EmpPlusSalGradePL>>(data);

            return PartialView("_Subordinates", emps);
        }

        public ActionResult GetHierarchy()
        {
            var data = _service.GetEmpsHierarchy(null);
            var emps = _mapper.Map<IEnumerable<EmpPlusSalGradePL>>(data);

            return View(emps);
        }


        public ActionResult Create()
        {
            var depts = _deptService.GetAll();
            var mgr = _service.GetAll();

            var Depts = new SelectList(depts, "DeptNo", "DeptName");
            var Mgr = new SelectList(mgr, "EmpNo", "EmpName");
            var Job = new SelectList(mgr.GroupBy(x => x.Job).Select(g => g.First()), "Job", "Job");
            var model = new EmpCreatePL { ListDept = Depts, ListMgr = Mgr, ListJob = Job };

            return PartialView("_Create", model);
        }

        [HttpPost]
        public ActionResult Create(EmpCreatePL empPL)
        {
            var model = _mapper.Map<EmpBL>(empPL);

            if (string.IsNullOrEmpty(empPL.EmpName))
            {
                ModelState.AddModelError("EmpName", "Required to fill");
            }
            else
            {
                var data2 = _service.GetAll().Where(e => e.EmpName == empPL.EmpName);
                var targetEmp = _service.GetEmp(empPL.EmpNo);

                if (data2.Count() != 0)
                {
                    ModelState.AddModelError("EmpName", "This name is already taken");
                }
            }

            if (string.IsNullOrEmpty(empPL.Job))
            {
                ModelState.AddModelError("Job", "Select Job");
            }

            if (string.IsNullOrEmpty(empPL.HireDate.ToString()))
            {
                ModelState.AddModelError("HireDate", "Select HireDate");
            }

            if (string.IsNullOrEmpty(empPL.Sal.ToString()))
            {
                ModelState.AddModelError("Sal", "Required to fill");
            }
            else if (empPL.Sal < 700 || empPL.Sal > 9999)
            {
                ModelState.AddModelError("Sal", "Min value Sal = 700, Max value Sal = 9999");
            }

            if (string.IsNullOrEmpty(empPL.DeptNo.ToString()))
            {
                ModelState.AddModelError("DeptNo", "Select Department");
            }

            if (ModelState.IsValid)
            {
                _service.Create(model);

                return RedirectToAction("DetailData", "MasterDetail", new { deptNo = empPL.DeptNo });
            }

            var depts = _deptService.GetAll();
            var mgr = _service.GetAll();

            var Depts = new SelectList(depts, "DeptNo", "DeptName");
            var Mgr = new SelectList(mgr, "EmpNo", "EmpName");
            var Job = new SelectList(mgr.GroupBy(x => x.Job).Select(g => g.First()), "Job", "Job");
            var data = _service.GetEmp(empPL.EmpNo);

            empPL.ListDept = Depts;
            empPL.ListJob = Job;
            empPL.ListMgr = Mgr;

            return PartialView("_Create", empPL);
        }

        public ActionResult Edit(decimal empNo)
        {
            var depts = _deptService.GetAll();
            var mgr = _service.GetAll();

            var Depts = new SelectList(depts, "DeptNo", "DeptName");
            var Mgr = new SelectList(mgr, "EmpNo", "EmpName");
            var Job = new SelectList(mgr.GroupBy(x => x.Job).Select(g => g.First()), "Job", "Job");
            var data = _service.GetEmp(empNo);
            var model = _mapper.Map<EmpCreatePL>(data);

            model.ListDept = Depts;
            model.ListJob = Job;
            model.ListMgr = Mgr;

            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(EmpCreatePL empPL)
        {
            var model = _mapper.Map<EmpBL>(empPL);

            if (string.IsNullOrEmpty(empPL.EmpName))
            {
                ModelState.AddModelError("EmpName", "Required to fill");
            }
            else
            {
                var data2 = _service.GetAll().Where(e => e.EmpName == empPL.EmpName);
                var targetEmp = _service.GetEmp(empPL.EmpNo);

                if (data2.Count() != 0 & targetEmp.EmpName != empPL.EmpName)
                {
                    ModelState.AddModelError("EmpName", "This name is already taken");
                }
            }

            if (string.IsNullOrEmpty(empPL.Job))
            {
                ModelState.AddModelError("Job", "Select Job");
            }

            if (string.IsNullOrEmpty(empPL.HireDate.ToString()))
            {
                ModelState.AddModelError("HireDate", "Select HireDate");
            }

            if (string.IsNullOrEmpty(empPL.Sal.ToString()))
            {
                ModelState.AddModelError("Sal", "Required to fill");
            } else if (empPL.Sal < 700 || empPL.Sal > 9999)
            {
                ModelState.AddModelError("Sal", "Min value Sal = 700, Max value Sal = 9999");
            }

            if (string.IsNullOrEmpty(empPL.DeptNo.ToString()))
            {
                ModelState.AddModelError("DeptNo", "Select Department");
            }

            if (ModelState.IsValid)
            {
                _service.Edit(model);
                return RedirectToAction("DetailData", "MasterDetail", new { deptNo = empPL.DeptNo });
            }

            var depts = _deptService.GetAll();
            var mgr = _service.GetAll();

            var Depts = new SelectList(depts, "DeptNo", "DeptName");
            var Mgr = new SelectList(mgr, "EmpNo", "EmpName");
            var Job = new SelectList(mgr.GroupBy(x => x.Job).Select(g => g.First()), "Job", "Job");
            var data = _service.GetEmp(empPL.EmpNo);

            empPL.ListDept = Depts;
            empPL.ListJob = Job;
            empPL.ListMgr = Mgr;

            return PartialView("_Edit", empPL);
        }

        [HttpDelete]
        public ActionResult Delete(decimal empNo)
        {
            _service.Delete(empNo);

            return new EmptyResult();
        }
    }
}
