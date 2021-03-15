using QLCV.Models;
using QLCV.Ulti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCV.Controllers
{
    public class DocumentController : Controller
    {
        private readonly ChuyenDeDBcontext _dbcontext;
        public DocumentController()
        {
            this._dbcontext = QLCVDbcontext.getInstance();
        }
        // GET: Document
        [CustomAuthen]
        public ActionResult ThongKeTiLeHoanThanh(int projectId = 0)
        {
            var data = (from a in _dbcontext.Projects
                        join b in _dbcontext.Jobs on a.Id equals b.ProjectId
                        join c in _dbcontext.Employees on b.EmployeeId equals c.Id
                        where projectId == 0 || a.Id == projectId
                        select new DocumentViewModel()
                        {
                            EmployeeName = c.Name,
                            ProjectName = a.Name,
                            StatusJob = b.Status,
                            EmployeeId = c.Id

                        }
                        ).ToList();
            var employee = _dbcontext.Employees.Where(x => x.Status == 1).ToList();
            var _return = new List<ResourceDocument>();
            foreach (var item in employee)
            {

                var listToDo = data.Where(x => x.StatusJob == (byte)StatusEnum.ToDo && x.EmployeeId == item.Id);
                var listInProgress = data.Where(x => x.StatusJob == (byte)StatusEnum.InProgress && x.EmployeeId == item.Id);
                var listDone = data.Where(x => x.StatusJob == (byte)StatusEnum.Done && x.EmployeeId == item.Id);
                double total = data.Where(x => x.EmployeeId == item.Id).Count();
                var resItem = new ResourceDocument()
                {
                    EmployeeName = item.Name,
                    Total = total,
                    Done = listDone.Count(),
                    ToDo = listToDo.Count(),
                    InProgress = listInProgress.Count(),
                };
                _return.Add(resItem);
            }

            return View("ti_le_hoan_thanh", _return);
        }

    }
}