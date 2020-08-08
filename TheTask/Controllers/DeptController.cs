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
    public class DeptController : Controller
    {
        private readonly IDeptService _service;
        private readonly IMapper _mapper;

        public DeptController(IDeptService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        public ActionResult Index()
        {
            var data = _service.GetAll();
            var depts = _mapper.Map<IEnumerable<DeptPL>>(data);

            return View(depts);
        }


        public ActionResult Details(int id)
        {
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DeptPL deptPL)
        {
            var model = _mapper.Map<DeptBL>(deptPL);

            if (string.IsNullOrEmpty(deptPL.DeptName))
            {
                ModelState.AddModelError("DeptName", "ne corect DeptName");
            }
            else
            {
                var data = _service.GetAll().Where(d => d.DeptName == deptPL.DeptName);

                if (data.Count() != 0)
                {
                    ModelState.AddModelError("DeptName", "ne corect DeptName2");
                }
            }
            if (string.IsNullOrEmpty(deptPL.Loc))
            {
                ModelState.AddModelError("Loc", "ne corect Loc");
            }

            if (ModelState.IsValid)
            {
                _service.Create(model);

                return RedirectToAction("Index");
            }

            return View(deptPL);
        }


        public ActionResult Edit(decimal deptNo)
        {
            var data = _service.GetDept(deptNo);
            var model = _mapper.Map<DeptPL>(data);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DeptPL deptPL)
        {
            var model = _mapper.Map<DeptBL>(deptPL);

            if (string.IsNullOrEmpty(deptPL.DeptName))
            {
                ModelState.AddModelError("DeptName", "ne corect DeptName");
            } else
            {
                var data = _service.GetAll().Where(d => d.DeptName == deptPL.DeptName);
                var targetDept = _service.GetDept(deptPL.DeptNo);

                if (data.Count() != 0 & targetDept.DeptName != deptPL.DeptName)
                {
                    ModelState.AddModelError("DeptName", "ne corect DeptName2");
                }
            }
            if (string.IsNullOrEmpty(deptPL.Loc))
            {
                ModelState.AddModelError("Loc", "ne corect Loc");
            }

            if (ModelState.IsValid)
            {
                _service.Edit(model);

                return RedirectToAction("Index");
            }

            return View(deptPL);
        }


        [HttpDelete]
        public ActionResult Delete(decimal deptNo)
        {
            _service.Delete(deptNo);

            return new EmptyResult();
        }
    }
}
