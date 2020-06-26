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
    public class EmpController : Controller
    {

        private readonly IEmpService _service;
        private readonly IMapper _mapper;

        public EmpController(IEmpService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: Emp
        public ActionResult Index()
        {
            var data = _service.GetAll();
            var emps = _mapper.Map<IEnumerable<EmpPL>>(data);

            return View(emps);
        }

        public ActionResult GetSubordinates(decimal? MgrNo)
        {
            var data = _service.GetEmpsHierarchy(MgrNo);
            var emps = _mapper.Map<IEnumerable<EmpPL>>(data);

            return PartialView("_Subordinates", emps);
        }

        public ActionResult GetHierarchy()
        {
            var data = _service.GetEmpsHierarchy(null);
            var emps = _mapper.Map<IEnumerable<EmpPL>>(data);

            return View(emps);
        }

        // GET: Emp/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Emp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Emp/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Emp/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Emp/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Emp/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Emp/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
