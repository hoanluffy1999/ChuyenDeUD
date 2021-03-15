﻿using QLCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCV.Controllers
{
    public class RateController : Controller
    {
        // GET: Rate
        private readonly ChuyenDeDBcontext _dbcontext;
        public RateController()
        {
            this._dbcontext = QLCVDbcontext.getInstance();
        }
        [CustomAuthen]
        public ActionResult Index()
        {
            ViewBag.title = "Danh sách đánh giá";
            return View();
        }
        [HttpGet]
        public ActionResult GetList(string name)
        {
            var data = _dbcontext.Rates.Where(x => /*x.Status == 1 &&*/( x.Name.ToLower().Contains(name.ToLower()) || string.IsNullOrEmpty(name))).ToList();
            return PartialView(data);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Rate inputModel)
        {
            
            _dbcontext.Rates.Add(inputModel);
            _dbcontext.SaveChanges();
            return Json(new { result = true }); ;
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            var entity = _dbcontext.Rates.Find(id);
            return PartialView(entity);
        }
        [HttpPost]
        public ActionResult Update(Rate inputModel)
        {


            var entity = _dbcontext.Rates.Find(inputModel.Id);
            if (entity == null)
            {
                return Json(new { result = false });
            }
            //inputModel.Status = 1;
            _dbcontext.Entry(entity).CurrentValues.SetValues(inputModel);
            _dbcontext.SaveChanges();
            return Json(new { result = true });
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {


            var entity = _dbcontext.Rates.Find(id);

            if (entity == null)
            {
                return Json(new { result = false });
            }
            //entity.Status = 0;
            _dbcontext.Entry(entity).CurrentValues.SetValues(entity);
            _dbcontext.SaveChanges();
            return Json(new { result = true }); ;
        }
    }
}