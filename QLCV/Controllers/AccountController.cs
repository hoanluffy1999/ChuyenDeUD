using QLCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCV.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private readonly ChuyenDeDBcontext _dbcontext;
        public AccountController()
        {
            this._dbcontext = QLCVDbcontext.getInstance();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(LoginModel mode)
        {
            var data = _dbcontext.Employees.Where(x => x.UserName.Equals(mode.UserName) && x.PassWord.Equals(mode.PassWord)).FirstOrDefault();
            if(data != null)
            {
                Session["Account"] = new Employee();

                Session["Account"] = data;
                return Json(new { result = true });
            }
            return Json(new { result = false });
        }
    }

}