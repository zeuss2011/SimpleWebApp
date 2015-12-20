using Microsoft.Practices.Unity;
using SimpleWebApp.Common;
using SimpleWebApp.WebServices.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleWebApp.WebServices.Controllers
{
    [AuthorizationRequired]
    [RoutePrefix("api/Produits")]
    public class ProduitsController : ApiController
    {
        #region Services

        //[Dependency]
        //public IUtilisateurService UtilisateurService { get; set; }

        [Dependency]
        public IProduitService ProduitService { get; set; }

        #endregion Services

        #region Public Constructor

        /// <summary>
        /// Public constructor 
        /// </summary>
        public ProduitsController()
        {
            //Configuration Unity
            UnityInjector.ConfigureMe(typeof(ProduitsController), this);
        }

        #endregion

        // GET api/product
        [Route("allproducts")]
        [Route("all")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var produits = ProduitService.GetProduits();
            if (produits != null && produits.Any())
                return Request.CreateResponse(HttpStatusCode.OK, produits);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Aucun produit trouvé");
        }

        // GET api/product/5
        [Route("productid/{id?}")]
        [Route("particularproduct/{id?}")]
        [Route("myproduct/{id:range(1, 3)}")]
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var produit = ProduitService.GetProduitById(id);
            if (produit != null)
                return Request.CreateResponse(HttpStatusCode.OK, produit);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Aucun produit trouvé pour id=" + id);
        }

        // POST api/product
        [Route("Create")]
        [Route("Register")]
        [HttpPost]
        public int Post([FromBody] Produit produit)
        {
            return ProduitService.CreerProduit(produit);
        }

        // PUT api/product/5
        [Route("Update/productid/{id}")]
        [Route("Modify/productid/{id}")]
        [HttpPut]
        public bool Put(int id, [FromBody] Produit produit)
        {
            if (id > 0)
            {
                return ProduitService.EnregistrerProduit(produit);
            }
            return false;
        }

        // DELETE api/product/5
        [Route("Remove/productid/{id}")]
        [Route("Clear/productid/{id}")]
        [HttpDelete]
        public bool Delete(int id)
        {
            if (id > 0)
                return ProduitService.SupprimerProduit(id);
            return false;
        }
    }
}
