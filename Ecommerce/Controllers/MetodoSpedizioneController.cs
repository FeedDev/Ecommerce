using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class MetodoSpedizioneController : Controller
    {
        ModelDbContext _dbContext = new ModelDbContext();


        public ActionResult Index()
        {
            return View(_dbContext.MetodoSpedizione.ToList());
        }


        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(MetodoSpedizione c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.MetodoSpedizione.Add(c);
                    _dbContext.SaveChanges();

                    MetodoSpedizione m = new MetodoSpedizione();
                    return RedirectToAction("Index");
                }
                else
                {
                    MetodoSpedizione m = new MetodoSpedizione();
                    return PartialView(m);

                }
            }
            catch
            {
                MetodoSpedizione m = new MetodoSpedizione();
                return PartialView(m);

            }
        }

        public JsonResult DeleteJson(int idMetodo)
        {
            try
            {
                _dbContext.MetodoSpedizione.Remove(_dbContext.MetodoSpedizione.Find(idMetodo));
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