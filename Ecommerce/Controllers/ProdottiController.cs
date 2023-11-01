using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class ProdottiController : Controller
    {

        ModelDbContext _dbContext = new ModelDbContext();

        public ActionResult Index()
        {
            return View(_dbContext.Prodotto.ToList());
        }

        public ActionResult ProdottiAdmin()
        {
            return View(_dbContext.Prodotto.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.IdCategoria = new SelectList(_dbContext.CategoriaProdotto.ToList(), "IdCategoria", "NomeCategoria");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Prodotto p)
        {
            try
            {
                if (p.PrezzoPrecedente != null)
                {
                    p.Promozione = true;
                }
                else
                {
                    p.Promozione = false;
                }

                if (p.Photo != null)
                {
                    if (p.Photo.ContentLength > 0)
                    {
                        string fileName = p.Photo.FileName;
                        string path = Server.MapPath("~/Content/img");
                        p.Photo.SaveAs($"{path}/{fileName}");
                        p.FotoUrl = p.Photo.FileName;
                    }
                }
                else
                {
                    p.FotoUrl = "default.jpg";
                }

                    _dbContext.Prodotto.Add(p);
                    _dbContext.SaveChanges();

                    return RedirectToAction("ProdottiAdmin");
            }
            catch(Exception ex)
            {
                ViewBag.IdCategoria = new SelectList(_dbContext.CategoriaProdotto.ToList(), "IdCategoria", "NomeCategoria");
                ViewBag.Errore = ex.Message;
                return View(p);
            }
            
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.IdCategoria = new SelectList(_dbContext.CategoriaProdotto.ToList(), "IdCategoria", "NomeCategoria");
            return View(_dbContext.Prodotto.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Prodotto p)
        {
            try
            {
                if (p.PrezzoPrecedente != null)
                {
                    p.Promozione = true;
                }
                else
                {
                    p.Promozione = false;
                }

                if (p.Photo != null)
                {
                    if (p.Photo.ContentLength > 0)
                    {
                        string fileName = p.Photo.FileName;
                        string path = Server.MapPath("~/Content/img");
                        p.Photo.SaveAs($"{path}/{fileName}");
                        p.FotoUrl = p.Photo.FileName;
                    }
                }

                if (ModelState.IsValid)
                {
                    _dbContext.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    _dbContext.SaveChanges();
                    return RedirectToAction("ProdottiAdmin");
                }
                else
                {
                    ViewBag.IdCategoria = new SelectList(_dbContext.CategoriaProdotto.ToList(), "IdCategoria", "NomeCategoria");
                    ViewBag.Errore = "Errore nella compilazione dei dati";
                    return View(p);
                }
            }
            catch(Exception ex)
            {
                ViewBag.IdCategoria = new SelectList(_dbContext.CategoriaProdotto.ToList(), "IdCategoria", "NomeCategoria");
                ViewBag.Errore = ex.Message;
                return View(p);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(_dbContext.Prodotto.Find(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                _dbContext.Prodotto.Remove(_dbContext.Prodotto.Find(id));
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Errore = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(_dbContext.Prodotto.Find(id));
        }

        public JsonResult AggiungiAlCarrello(int idprodotto, int qtprodotto)
        {
            int idCliente = Convert.ToInt32(Request.Cookies["USER_COOKIE"]["ID"]);

            Carrello c = _dbContext.Carrello.Where(x => x.IdCliente == idCliente && x.Scaduto == false).FirstOrDefault();

            if (c == null)
            {
                c = new Carrello();
                c.IdCliente = idCliente;

                _dbContext.Carrello.Add(c);
            }

            if(qtprodotto <= 0)
            {
                qtprodotto = 1;
            }

            RecordOrdine r = new RecordOrdine();
            r.IdProdotto = idprodotto;
            r.QtProdotto = qtprodotto;
            r.IdCarrello = c.IdCarrello;

            if(_dbContext.RecordOrdine.Where(x=> x.IdProdotto == r.IdProdotto && x.IdCarrello == r.IdCarrello).FirstOrDefault() != null)
            {
                r = _dbContext.RecordOrdine.Where(x => x.IdProdotto == r.IdProdotto && x.IdCarrello == r.IdCarrello).FirstOrDefault();
                r.QtProdotto += qtprodotto;
                if (r.QtProdotto > r.Prodotto.QtStock)
                {
                    return Json("nondisponibile", JsonRequestBehavior.AllowGet);
                }
                _dbContext.Entry(r).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                Prodotto p = _dbContext.Prodotto.Where(x => x.IdProdotto == r.IdProdotto).FirstOrDefault();
                r.Prodotto = p;
                if (r.Prodotto.QtStock <= qtprodotto)
                {
                    return Json("nondisponibile", JsonRequestBehavior.AllowGet);
                }
                _dbContext.RecordOrdine.Add(r);
            }

            _dbContext.SaveChanges();

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public JsonResult RimuoviCarrello(int idrecord)
        {
            decimal totalecarrello = 0;
            int idCliente = Convert.ToInt32(Request.Cookies["USER_COOKIE"]["ID"]);
            Carrello c = _dbContext.Carrello.FirstOrDefault(x => x.IdCliente == idCliente && x.Scaduto == false);

            RecordOrdine r =_dbContext.RecordOrdine.FirstOrDefault(x => x.IdRecordOrdine == idrecord && x.IdCarrello == c.IdCarrello);

            _dbContext.RecordOrdine.Remove(r);
            _dbContext.SaveChanges();

            List<RecordOrdine> listaRecord = _dbContext.RecordOrdine.Where(x => x.IdCarrello == c.IdCarrello).ToList();
            foreach (RecordOrdine record in listaRecord)
            {
                totalecarrello += record.Prodotto.Prezzo * record.QtProdotto;
            }

            int count = _dbContext.RecordOrdine.Where(x=> x.IdCarrello == c.IdCarrello).Count();
            return Json(totalecarrello.ToString("C2"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SvuotaCarrello()
        {
            int idCliente = Convert.ToInt32(Request.Cookies["USER_COOKIE"]["ID"]);
            Carrello c = _dbContext.Carrello.FirstOrDefault(x => x.IdCliente == idCliente && x.Scaduto == false);

            List<RecordOrdine> r = _dbContext.RecordOrdine.Where(x=> x.IdCarrello == c.IdCarrello).ToList();

            foreach(RecordOrdine record in r)
            {
                _dbContext.RecordOrdine.Remove(record);
            }

            _dbContext.SaveChanges();

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public JsonResult FiltraProdottiByCategoria(int[] Categorie)
        {
            try
            {
                if (Categorie != null)
                {
                    List<ProdottoJson> ListToReturn = new List<ProdottoJson>();

                    for (int i = 0; i < Categorie.Length; i++)
                    {
                        int id = Categorie[i];
                        List<Prodotto> listaProdotti = _dbContext.Prodotto.Where(x => x.IdCategoria == id).ToList();
                        
                        if (listaProdotti != null)
                        {
                            foreach(Prodotto prodotto in listaProdotti)
                            {
                                ProdottoJson prodottoToPrint = new ProdottoJson();
                                prodottoToPrint.IdProdotto = prodotto.IdProdotto;
                                prodottoToPrint.NomeProdotto = prodotto.NomeProdotto;
                                prodottoToPrint.FotoUrl = prodotto.FotoUrl;
                                prodottoToPrint.PrezzoPrecedente = prodotto.PrezzoPrecedente;
                                prodottoToPrint.Prezzo = prodotto.Prezzo;
                                ListToReturn.Add(prodottoToPrint);
                            }
                        }
                    }

                    return Json(ListToReturn, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("ko", JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }



        }

        public JsonResult PlusButton(int idRecord)
        {
            try
            {
                List<string> response = new List<string>();
                RecordOrdine r = _dbContext.RecordOrdine.FirstOrDefault(x => x.IdRecordOrdine == idRecord);
                r.QtProdotto += 1;
                decimal tot = 0;
                decimal totalecarrello = 0;

                if (r.QtProdotto > r.Prodotto.QtStock)
                {
                    return Json("nondisponibile", JsonRequestBehavior.AllowGet);
                }

                if (r.QtProdotto <= 0)
                {
                    r.QtProdotto = 0;
                    _dbContext.RecordOrdine.Remove(r);
                    response.Add("");
                }
                else
                {
                    _dbContext.Entry(r).State = System.Data.Entity.EntityState.Modified;
                    tot = r.Prodotto.Prezzo * r.QtProdotto;
                    response.Add(tot.ToString("C2"));
                }

                _dbContext.SaveChanges();

                int idCliente = Convert.ToInt32(Request.Cookies["USER_COOKIE"]["ID"]);
                Carrello c = _dbContext.Carrello.FirstOrDefault(x => x.IdCliente == idCliente && x.Scaduto == false);
                List<RecordOrdine> listaRecord = _dbContext.RecordOrdine.Where(x => x.IdCarrello == c.IdCarrello).ToList();
                foreach (RecordOrdine record in listaRecord)
                {
                    totalecarrello += record.Prodotto.Prezzo * record.QtProdotto;
                }

                if (c.CodiceSconto != null)
                {
                    totalecarrello -= totalecarrello * c.CodiceSconto.Percentuale / 100;
                }

                response.Add(totalecarrello.ToString("C2"));

                return Json(response.ToArray(), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult MinusButton(int idRecord)
        {
            try
            {
                List<string> response = new List<string>();
                RecordOrdine r = _dbContext.RecordOrdine.FirstOrDefault(x => x.IdRecordOrdine == idRecord);
                r.QtProdotto -= 1;
                decimal tot = 0;
                decimal totalecarrello = 0;

                if (r.QtProdotto > r.Prodotto.QtStock)
                {
                    return Json("nondisponibile", JsonRequestBehavior.AllowGet);
                }

                if (r.QtProdotto <= 0)
                {
                    r.QtProdotto = 0;
                    response.Add("");
                    _dbContext.RecordOrdine.Remove(r);
                }
                else
                {
                    _dbContext.Entry(r).State = System.Data.Entity.EntityState.Modified;
                    tot = r.Prodotto.Prezzo * r.QtProdotto;
                    response.Add(tot.ToString("C2"));
                }

                _dbContext.SaveChanges();

                int idCliente = Convert.ToInt32(Request.Cookies["USER_COOKIE"]["ID"]);
                Carrello c = _dbContext.Carrello.FirstOrDefault(x => x.IdCliente == idCliente && x.Scaduto == false);
                List<RecordOrdine> listaRecord = _dbContext.RecordOrdine.Where(x => x.IdCarrello == c.IdCarrello).ToList();
                foreach (RecordOrdine record in listaRecord)
                {
                    totalecarrello += record.Prodotto.Prezzo * record.QtProdotto;
                }

                if (c.CodiceSconto != null)
                {
                    totalecarrello -= totalecarrello * c.CodiceSconto.Percentuale / 100;
                }

                response.Add(totalecarrello.ToString("C2"));

                return Json(response.ToArray(), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [ChildActionOnly]
        public ActionResult RicercaProdotti()
        {
            return PartialView();
        }

        [HttpGet]
        public JsonResult RicercaProdotti(string testo)
        {
            List<Prodotto> p = _dbContext.Prodotto.Where(x=> x.NomeProdotto.Contains(testo)).ToList();

            List<ProdottoJson> listJson = new List<ProdottoJson>();
            foreach(Prodotto prodotto in p)
            {
                ProdottoJson prodottoJson = new ProdottoJson();
                prodottoJson.IdProdotto = prodotto.IdProdotto;
                prodottoJson.NomeProdotto = prodotto.NomeProdotto;
                listJson.Add(prodottoJson);
            }

            return Json(listJson, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AddSconto(string codiceSconto)
        {
            CodiceSconto c = _dbContext.CodiceSconto.FirstOrDefault(x => x.Codice == codiceSconto);
            if(c.Scadenza >= DateTime.Now && c.Usato == false)
            {
                c.Usato = true;

                Cliente cliente = _dbContext.Cliente.FirstOrDefault(x => x.IdCliente == c.IdCliente);
                Carrello carrello = cliente.Carrello.Where(x => x.Scaduto == false).FirstOrDefault();
                carrello.CodiceSconto = c;

                _dbContext.Entry(carrello).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();

                return Json("", JsonRequestBehavior.AllowGet);
            }

            return Json("ko", JsonRequestBehavior.AllowGet);
        }
    }
}