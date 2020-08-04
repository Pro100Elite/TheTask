using AutoMapper;
using BL.Interfaces;
using BL.Models;
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

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EmpCreatePL empPL)
        {
            var model = _mapper.Map<EmpBL>(empPL);

            _service.Create(model);

            return RedirectToAction("Index");
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

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EmpCreatePL empPL)
        {
            var model = _mapper.Map<EmpBL>(empPL);

            _service.Edit(model);

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public ActionResult Delete(decimal empNo)
        {
            _service.Delete(empNo);

            return new EmptyResult();
        }


        public JsonResult CheckEmpNo(decimal? empNo)
        {
            var data = _service.GetEmp(empNo);
            if (data.EmpNo == 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            string error = String.Format(CultureInfo.InvariantCulture,
                "{0} is not available.", empNo);

            return Json(error, JsonRequestBehavior.AllowGet);

        }
    }
}
