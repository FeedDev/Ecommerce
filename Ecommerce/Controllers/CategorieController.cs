using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class CategorieController : Controller
    {
        ModelDbContext _dbContext = new ModelDbContext();


        public ActionResult Index()
        {
            return RedirectToAction("Create");
        }


        public ActionResult Create() 
        {
            return View(_dbContext.CategoriaProdotto.ToList());
        }

        public JsonResult CreateJson(CategoriaProdotto c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.CategoriaProdotto.Add(c);
                    _dbContext.SaveChanges();

                    return Json(c, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("ko", JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("ko", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteJson(int idCategoria)
        {
            try
            {
                _dbContext.CategoriaProdotto.Remove(_dbContext.CategoriaProdotto.Find(idCategoria));
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