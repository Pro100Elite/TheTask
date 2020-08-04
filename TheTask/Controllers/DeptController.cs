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

            _service.Create(model);

            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
           

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult CheckDeptName(string deptName)
        {
            var data = _service.GetAll().Where(d => d.DeptName == deptName);
            if (data.Count() == 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            string error = String.Format(CultureInfo.InvariantCulture,
                "{0} is not available.", deptName);

            return Json(error, JsonRequestBehavior.AllowGet);

        }
    }
}
