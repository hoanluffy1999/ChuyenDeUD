using QLCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCV.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        private readonly ChuyenDeDBcontext _dbcontext;
        public DepartmentController()
        {
            this._dbcontext = QLCVDbcontext.getInstance();
        }
        public ActionResult Index()
        {
            ViewBag.title = "Danh sách phòng ban";
            return View();
        }
        [HttpGet]
        public ActionResult GetList(string name)
        {
            var data = _dbcontext.Departments.Where(x => x.Status == 1 &&( x.Name.ToLower().Contains(name.ToLower()) || string.IsNullOrEmpty(name))).ToList();
            return PartialView(data);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Department inputModel)
        {
            inputModel.Status = 1;
            _dbcontext.Departments.Add(inputModel);
            _dbcontext.SaveChanges();
            return Json(new { result = true }); ;
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            var entity = _dbcontext.Departments.Find(id);
            return PartialView(entity);
        }
        [HttpPost]
        public ActionResult Update(Department inputModel)
        {


            var entity = _dbcontext.Departments.Find(inputModel.Id);
            if (entity == null)
            {
                return Json(new { result = false });
            }
            inputModel.Status = 1;
            _dbcontext.Entry(entity).CurrentValues.SetValues(inputModel);
            _dbcontext.SaveChanges();
            return Json(new { result = true });
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {


            var entity = _dbcontext.Departments.Find(id);

            if (entity == null)
            {
                return Json(new { result = false });
            }
            entity.Status = 0;
            _dbcontext.Entry(entity).CurrentValues.SetValues(entity);
            _dbcontext.SaveChanges();
            return Json(new { result = true }); ;
        }
    }
}