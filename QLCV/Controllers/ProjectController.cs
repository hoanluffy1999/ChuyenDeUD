using QLCV.Models;
using QLCV.Models.ViewModel;
using QLCV.Ulti;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCV.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        private readonly ChuyenDeDBcontext _dbcontext;
        public ProjectController()
        {
            this._dbcontext = QLCVDbcontext.getInstance();
        }
        public ActionResult Index()
        {
            ViewBag.title = "Danh sách dự án";
            return View();
        }
        [HttpGet]
        public ActionResult GetList(string name)
        {
            var data = _dbcontext.Projects.Where(x => x.Status == 1 &&( x.Name.ToLower().Contains(name.ToLower()) || string.IsNullOrEmpty(name))).ToList();
            return PartialView(data);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Project inputModel)
        {
            inputModel.Status = 1;
            _dbcontext.Projects.Add(inputModel);
            _dbcontext.SaveChanges();
            return Json(new { result = true }); ;
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            var entity = _dbcontext.Projects.Find(id);
            return PartialView(entity);
        }
        [HttpPost]
        public ActionResult Update(Project inputModel)
        {

            var entity = _dbcontext.Projects.Find(inputModel.Id);
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
            var entity = _dbcontext.Projects.Find(id);

            if (entity == null)
            {
                return Json(new { result = false });
            }
            entity.Status = 0;
            _dbcontext.Entry(entity).CurrentValues.SetValues(entity);
            _dbcontext.SaveChanges();
            return Json(new { result = true }); ;
        }
        [HttpGet]
        public ActionResult DetailProject(int id)
        {
            var data = _dbcontext.Projects.FirstOrDefault(x => x.Id == id);
            return View(data);
        }
        [HttpGet]
        public ActionResult AddEmployeetoProject()
        {
            var data = _dbcontext.Employees.Where(x => x.Status == 1).ToList();
            return PartialView(data);
        }
        [HttpPost]
        public ActionResult AddEmployeetoProject(List<ProjectEmployee> model)
        {
            if(model != null)
            {
                foreach(var item in model)
                {
                    _dbcontext.ProjectEmployees.Add(item);
                    _dbcontext.SaveChanges();
                }    
            }    
            return Json(new { result=true});
        }
        [HttpGet]
        public ActionResult AddTask(int id)
        {
            //var data = from a in _dbcontext.ProjectEmployees
            //           join b in _dbcontext.Employees on a.EmployId equals b.Id
            //           where a.ProjectId == id && b.Status == 1
            //           select b;
            //data.ToList();
            var data = _dbcontext.Employees.Where(x => x.Status == 1);
            ViewData["NhanVien"] = data;
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddTask(Job model)
        {
            try
            {
                model.Status = 1;
                _dbcontext.Jobs.Add(model);
                _dbcontext.SaveChanges();
                return Json(new { result = true });

            }catch(Exception ex)
            {
                return Json(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult Employee(int id)
        {
            var data = _dbcontext.Employees.FirstOrDefault(x => x.Id == id);
            return PartialView(data);
        }
        [HttpGet]
        public ActionResult ListTasks(int id)
        {
            //var data = _dbcontext.Jobs.Where(x => x.ProjectId == id).ToList();
            var data = (from a in _dbcontext.Jobs
                       join b in _dbcontext.Employees
                       on a.EmployeeId equals b.Id
                       where a.ProjectId == id
                       select new JobViewModel()
                       {
                           Id = a.Id,
                           Name = a.Name,
                           EmployeeId = a.EmployeeId,
                           Description = a.Description,
                           Status = a.Status,
                           EmployeeName = b.Name
                       }).ToList();
          
            return PartialView(data);
        }
        [HttpGet]
        public ActionResult ListEmployee(int id)
        {
            var data = from a in _dbcontext.ProjectEmployees
                       join b in _dbcontext.Employees on a.EmployId equals b.Id
                       where a.ProjectId == id && b.Status == 1
                       select b;
            data.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult UpdateJob(int jobId)
        {
            var data = _dbcontext.Jobs.FirstOrDefault(x => x.Id == jobId);
            return PartialView(data);
        }
        [HttpPost]
        public ActionResult UpdateJob(Job model)
        {
            var data = _dbcontext.Jobs.FirstOrDefault(x => x.Id == model.Id);
            //data.Description = model.Description;
            //data.Name = model.Name;
            data.Status = (byte) StatusEnum.InProgress;
            var entry = _dbcontext.Entry(data); 
            entry.State = EntityState.Modified;
            _dbcontext.SaveChanges();
            return Json(data);
        }
        [HttpGet]
        public ActionResult UpdateJobDone(int jobId)
        {
            var data = _dbcontext.Jobs.FirstOrDefault(x => x.Id == jobId);
            return PartialView(data);
        }
        [HttpPost]
        public ActionResult UpdateJobDone(Job model)
        {
            var data = _dbcontext.Jobs.FirstOrDefault(x => x.Id == model.Id);
            //data.Description = model.Description;
            //data.Name = model.Name;
            data.Status = (byte)StatusEnum.Done;
            var entry = _dbcontext.Entry(data);
            entry.State = EntityState.Modified;
            _dbcontext.SaveChanges();
            return Json(data);
        }
        [HttpGet]
        public ActionResult updatestatus(int id)
        {
            ViewData["JobId"] = id;
            var data = _dbcontext.Rates.Where(x => true).ToList();
            return PartialView(data);
        }
        [HttpPost]
        public ActionResult updatestatus(Job model )
        {
            var data = _dbcontext.Jobs.Where(x =>x.Id == model.Id).FirstOrDefault();
            data.RateId1 = model.RateId1;
            var entry = _dbcontext.Entry(data);
            entry.State = EntityState.Modified;
            _dbcontext.SaveChanges();
            return Json(data);
        }
    }
}