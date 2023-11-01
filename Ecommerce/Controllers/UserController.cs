using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ecommerce.Controllers
{
    public class UserController : Controller
    {
        ModelDbContext _dbContext = new ModelDbContext();

        // GET: User
        public ActionResult Index()
        {
            return RedirectToAction("UserLogin");
        }

        public ActionResult UserRegister()
        {
            if (!User.Identity.IsAuthenticated)
                return View();
            else
                return RedirectToAction("Index", "Prodotti");
        }

        [HttpPost]
        public ActionResult UserRegister(string Username, string Password, Cliente c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Login l = new Login();
                    l.Username = Username;   
                    l.Password = Password;
                    l.Ruolo = "Utente";

                    _dbContext.Login.Add(l);
                    _dbContext.SaveChanges();

                    l = _dbContext.Login.FirstOrDefault(x=> x.Username == l.Username && x.Password == l.Password);
                    c.Login = l;

                    _dbContext.Cliente.Add(c);
                    _dbContext.SaveChanges();

                    FormsAuthentication.SetAuthCookie(l.Username, true);
                    HttpCookie cookiePropID = new HttpCookie("USER_COOKIE");
                    cookiePropID.Values["ID"] = c.IdCliente.ToString();
                    Response.Cookies.Add(cookiePropID);

                    TempData["Successo"] = "Registrazione effettuata con successo";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Dati inseriti non corretti";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult UserLogin()
        {
            if (!User.Identity.IsAuthenticated)
                return View();
            else
                return RedirectToAction("Index", "Prodotti");
        }

        [HttpPost]
        public ActionResult UserLogin(Login l)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Login user = _dbContext.Login.Where(x => x.Username == l.Username && x.Password == l.Password).FirstOrDefault();

                    if(user != null)
                    {
                        if(user.Ruolo != "Admin")
                        {
                            Cliente c = _dbContext.Cliente.Where(x => x.Login.IdUser == user.IdUser).FirstOrDefault();
                            HttpCookie cookiePropID = new HttpCookie("USER_COOKIE");
                            cookiePropID.Values["ID"] = c.IdCliente.ToString();
                            Response.Cookies.Add(cookiePropID);
                        }
                       
                        FormsAuthentication.SetAuthCookie(user.Username, true);

                        TempData["Successo"] = "Login effettuato con successo";

                        return Redirect(FormsAuthentication.DefaultUrl);
                    }
                    else
                    {
                        ViewBag.Error = "Utente non trovato";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Error = "Dati inseriti non corretti";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }


            return View();
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
        }


        public ActionResult Carrello()
        {
            int idCliente = Convert.ToInt32(Request.Cookies["USER_COOKIE"]["ID"]);

            Carrello c = _dbContext.Carrello.FirstOrDefault(x => x.IdCliente == idCliente && x.Scaduto == false);

            List<RecordOrdine> lista = new List<RecordOrdine>();

            if (c != null)
            {
                lista = _dbContext.RecordOrdine.Where(x => x.IdCarrello == c.IdCarrello).ToList();
            }
            else
            {
                c = new Carrello();
                c.IdCliente = idCliente;

                _dbContext.Carrello.Add(c);
            }
            decimal totale = 0;
            foreach(RecordOrdine r in lista)
            {
                totale += r.QtProdotto * r.Prodotto.Prezzo;
            }

            if(c.CodiceSconto != null)
            {
                totale -= totale * c.CodiceSconto.Percentuale / 100;
                ViewBag.CodiceSconto = c.CodiceSconto.Codice;
            }

            ViewBag.totale = totale.ToString("C2");
            return View(lista);
        }

        public ActionResult PaginaUtente()
        {
            int idCliente = Convert.ToInt32(Request.Cookies["USER_COOKIE"]["ID"]);

            return View(_dbContext.Cliente.FirstOrDefault(x=> x.IdCliente == idCliente));
        }

        public ActionResult CreateMetodo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMetodo(MetodoPagamento p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int idCliente = Convert.ToInt32(Request.Cookies["USER_COOKIE"]["ID"]);

                    p.IdCliente = idCliente;
                    _dbContext.MetodoPagamento.Add(p);
                    _dbContext.SaveChanges();

                    return RedirectToAction("PaginaUtente");

                }
                else
                {
                    ViewBag.Errore = "Errore nella compilazione dei dati";
                    return View(p);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Errore = ex.Message;
                return View(p);
            }

        }

        [HttpGet]
        public ActionResult EditMetodo(int id)
        {
            return View(_dbContext.MetodoPagamento.Find(id));
        }

        [HttpPost]
        public ActionResult EditMetodo(MetodoPagamento p)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _dbContext.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    _dbContext.SaveChanges();
                    return RedirectToAction("PaginaUtente");
                }
                else
                {
                    ViewBag.Errore = "Errore nella compilazione dei dati";
                    return View(p);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Errore = ex.Message;
                return View(p);
            }
        }

        [HttpGet]
        public ActionResult DeleteMetodo(int id)
        {
            return View(_dbContext.MetodoPagamento.Find(id));
        }

        [HttpPost]
        [ActionName("DeleteMetodo")]
        public ActionResult ConfirmDeleteMetodo(int id)
        {
            try
            {
                _dbContext.MetodoPagamento.Remove(_dbContext.MetodoPagamento.Find(id));
                _dbContext.SaveChanges();
                return RedirectToAction("PaginaUtente");
            }
            catch (Exception ex)
            {
                ViewBag.Errore = ex.Message;
                return View();
            }
        }


        public ActionResult CreateSpedizione()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSpedizione(IndirizzoSpedizione p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int idCliente = Convert.ToInt32(Request.Cookies["USER_COOKIE"]["ID"]);

                    p.IdCliente = idCliente;
                    _dbContext.IndirizzoSpedizione.Add(p);
                    _dbContext.SaveChanges();

                    return RedirectToAction("PaginaUtente");

                }
                else
                {
                    ViewBag.Errore = "Errore nella compilazione dei dati";
                    return View(p);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Errore = ex.Message;
                return View(p);
            }

        }

        [HttpGet]
        public ActionResult EditSpedizione(int id)
        {
            return View(_dbContext.IndirizzoSpedizione.Find(id));
        }

        [HttpPost]
        public ActionResult EditSpedizione(IndirizzoSpedizione p)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _dbContext.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    _dbContext.SaveChanges();
                    return RedirectToAction("PaginaUtente");
                }
                else
                {
                    ViewBag.Errore = "Errore nella compilazione dei dati";
                    return View(p);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Errore = ex.Message;
                return View(p);
            }
        }

        [HttpGet]
        public ActionResult DeleteSpedizione(int id)
        {
            return View(_dbContext.IndirizzoSpedizione.Find(id));
        }

        [HttpPost]
        [ActionName("DeleteSpedizione")]
        public ActionResult ConfirmDeleteSpedizione(int id)
        {
            try
            {
                _dbContext.IndirizzoSpedizione.Remove(_dbContext.IndirizzoSpedizione.Find(id));
                _dbContext.SaveChanges();
                return RedirectToAction("PaginaUtente");
            }
            catch (Exception ex)
            {
                ViewBag.Errore = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult DettagliOrdine(int id)
        {
            return View(_dbContext.Ordine.Find(id));
        }
    }
}