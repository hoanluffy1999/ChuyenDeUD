using QLCV.Models;
using QLCV.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCV.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ChuyenDeDBcontext _dbcontext;
        public EmployeeController()
        {
            this._dbcontext = QLCVDbcontext.getInstance();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetList(string name)
        {
            var data = (from a in _dbcontext.Employees 
                       join b in _dbcontext.Departments
                       on a.DepartmentId equals b.Id
                       where a.Status ==1 && (a.Name.ToLower().Contains(name.ToLower()) || string.IsNullOrEmpty(name))
                       select new EmployeeViewModel()
                       {
                           Id = a.Id,
                           Name = a.Name,
                           Birthday = a.Birthday,
                           Email = a.Email,
                           Phone = a.Phone,
                           Sex = a.Sex,
                           DepartmentName = b.Name
                       }).ToList();
                
                
                
                //_dbcontext.Employees.Where(x => x.Status == 1 && (x.Name.ToLower().Contains(name.ToLower()) || string.IsNullOrEmpty(name))).ToList();
            return PartialView(data);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewData["PhongBan"] = _dbcontext.Departments.Where(x => x.Status == 1).ToList();
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Employee inputModel)
        {
            inputModel.Status = 1;
            _dbcontext.Employees.Add(inputModel);
            _dbcontext.SaveChanges();
            return Json(new { result = true }); ;
        }
        [HttpGet]
        public ActionResult Update(int id)
        {

            var entity = _dbcontext.Employees.Find(id);
            ViewData["PhongBan"] = _dbcontext.Departments.Where(x => x.Status == 1).ToList();
            return PartialView(entity);
        }
        [HttpPost]
        public ActionResult Update(Employee inputModel)
        {
           

            var entity = _dbcontext.Employees.Find(inputModel.Id);
            if (entity == null)
            {
                return Json(new { result = false });
            }
           
            _dbcontext.Entry(entity).CurrentValues.SetValues(inputModel);
            _dbcontext.SaveChanges();
             return Json(new { result = true }); 
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {


            var entity = _dbcontext.Employees.Find(id);
            
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