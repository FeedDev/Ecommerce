using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class CodiceScontoController : Controller
    {
        ModelDbContext _dbContext = new ModelDbContext();

        public ActionResult Index()
        {
            return View(_dbContext.CodiceSconto.ToList());
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(CodiceSconto c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    c.Usato = false;
                    _dbContext.CodiceSconto.Add(c);
                    _dbContext.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return PartialView();
                }
            }
            catch
            {
                return PartialView();
            }
        }

        public ActionResult Edit(int id)
        {
            return View(_dbContext.CodiceSconto.FirstOrDefault(s=> s.IdCodiceSconto == id));
        }

        [HttpPost]
        public ActionResult Edit(CodiceSconto c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Entry(c).State = System.Data.Entity.EntityState.Modified;
                    _dbContext.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public JsonResult DeleteJson(int idCodice)
        {
            try
            {
                _dbContext.CodiceSconto.Remove(_dbContext.CodiceSconto.Find(idCodice));
                _dbContext.SaveChanges();
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.Errore = ex.Message;
                return Json("ko", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
