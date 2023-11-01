using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ecommerce.Controllers
{
    public class ValuesController : ApiController
    {
        ModelDbContext _modeldbContext = new ModelDbContext();

        [HttpGet]
        [Route("api/ProdottiOfferta")]
        public IEnumerable<ProdottoJson> ProdottiOfferta()
        {
            //RESTITUIRE TUTTI I PRODOTTI CHE SONO IN OFFERTA
            
            List<ProdottoJson> list = new List<ProdottoJson>();

            foreach(Prodotto p in _modeldbContext.Prodotto.Where(p => p.Promozione == true).ToList())
            {
                ProdottoJson json = new ProdottoJson();
                json.IdProdotto = p.IdProdotto;
                json.NomeProdotto = p.NomeProdotto;
                json.Prezzo = p.Prezzo;
                json.PrezzoPrecedente = p.PrezzoPrecedente;
                json.IdCategoria = p.IdCategoria;

                list.Add(json);
            }

            return list;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/values/5
        public void Delete(int id)
        {

        }
    }
}
