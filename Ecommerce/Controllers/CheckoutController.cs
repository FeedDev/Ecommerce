using Antlr.Runtime.Misc;
using Ecommerce.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class CheckoutController : Controller
    {
        ModelDbContext _dbContext = new ModelDbContext();

        // GET: Checkout
        public ActionResult Index(int IdCarrello)
        {
            int idCliente = Convert.ToInt32(Request.Cookies["USER_COOKIE"]["ID"]);
            ViewBag.Metodi = new SelectList(_dbContext.MetodoPagamento.Where(x=> x.IdCliente == idCliente).ToList(), "IdMetodoPagamento", "CodiceMetodo");
            ViewBag.Indirizzi = new SelectList(_dbContext.IndirizzoSpedizione.Where(x=> x.IdCliente == idCliente).ToList(), "IdIndirizzoSpedizione", "Indirizzo");
            ViewBag.Record = _dbContext.RecordOrdine.Where(x => x.IdCarrello == IdCarrello).ToList();
            ViewBag.Carrello = _dbContext.Carrello.Where(x => x.IdCarrello == IdCarrello).FirstOrDefault();

            List<SelectListItem> options = new List<SelectListItem>();

            foreach(MetodoSpedizione m in _dbContext.MetodoSpedizione.ToList())
            {
                SelectListItem item = new SelectListItem { Text = m.TipoSpedizione + " - " + m.Costo + "€", Value = m.IdMetodoSpedizione.ToString() };
                options.Add(item);
            }
            
            ViewBag.MetodiSpedizione = options;
            return View();
        }


        [HttpPost]
        public ActionResult Index(Ordine o) 
        {
            try
            {
                int idCliente = Convert.ToInt32(Request.Cookies["USER_COOKIE"]["ID"]);

                o.IdCliente = idCliente;
                o.IdCarrello = _dbContext.Carrello.FirstOrDefault(x => x.IdCliente == idCliente && x.Scaduto == false).IdCarrello;
                o.DataOrdine = DateTime.Now;

                if (ModelState.IsValid)
                {
                    foreach(RecordOrdine r in _dbContext.RecordOrdine.Where(x=> x.IdCarrello == o.IdCarrello))
                    {
                        o.TotaleOrdine += r.Prodotto.Prezzo * r.QtProdotto;
                        r.Prodotto.QtStock -= r.QtProdotto;
                    }
                    o.MetodoSpedizione = _dbContext.MetodoSpedizione.FirstOrDefault(x => x.IdMetodoSpedizione == o.IdMetodoSpedizione);

                    
                    Carrello c = _dbContext.Carrello.FirstOrDefault(x => x.IdCliente == idCliente && x.Scaduto == false);
                    if (c.CodiceSconto != null)
                    {
                        o.TotaleOrdine -= o.TotaleOrdine * c.CodiceSconto.Percentuale / 100;
                    }

                    c.Scaduto = true;
                    o.TotaleOrdine += o.MetodoSpedizione.Costo;

                    _dbContext.Ordine.Add(o);
                    _dbContext.Entry(c).State = System.Data.Entity.EntityState.Modified;
                    _dbContext.SaveChanges();

                    return RedirectToAction("PaginaUtente", "User");
                }
                else
                {
                    ViewBag.Errore = "Errore nella compilazione dei dati";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Errore = ex.Message;
                return View();
            }
        }
    }
}