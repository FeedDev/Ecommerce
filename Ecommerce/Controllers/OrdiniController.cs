using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class OrdiniController : Controller
    {
        ModelDbContext _dbContext = new ModelDbContext();

        public ActionResult Index()
        {

            List<Ordine> listOrdini = _dbContext.Ordine.ToList();
            listOrdini.Reverse();
            return View(listOrdini);
        }

        [HttpGet]
        public ActionResult Edit(int id, int IdCliente)
        {
            ViewBag.MetodiSpedizione = new SelectList(_dbContext.MetodoSpedizione.ToList(), "IdMetodoSpedizione", "TipoSpedizione");
            ViewBag.Metodi = new SelectList(_dbContext.MetodoPagamento.Where(x => x.IdCliente == IdCliente).ToList(), "IdMetodoPagamento", "CodiceMetodo");
            ViewBag.Indirizzi = new SelectList(_dbContext.IndirizzoSpedizione.Where(x => x.IdCliente == IdCliente).ToList(), "IdIndirizzoSpedizione", "Indirizzo");
            return View(_dbContext.Ordine.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Ordine o)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Entry(o).State = System.Data.Entity.EntityState.Modified;
                    _dbContext.SaveChanges();
                    return RedirectToAction("ProdottiAdmin");
                }
                else
                {
                    ViewBag.Errore = "Errore nella compilazione dei dati";
                    return View(0);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Errore = ex.Message;
                return View(0);
            }
        }
    }
}