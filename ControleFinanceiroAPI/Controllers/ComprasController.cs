using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ControleFinanceiroAPI.Models;
using System.Web.Http.Cors;
using ControleFinanceiroAPI.DAL;

namespace ControleFinanceiroAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ComprasController : ApiController
    {
        private ControleFinanceiro1Entities context;

        private ICompraRepository compraRepository;
        //private FinanceiroContext context;

        public ComprasController()
        {
            context = new ControleFinanceiro1Entities();
            this.compraRepository = new CompraRepository(context);
        }

        // GET: api/Compras
        public IQueryable<ComprasDTO> GetCompras()
        {

            var compras = compraRepository.GetCompras();
            return compras;

        }

        // GET: api/Compras/5
        [ActionName("getCompra")]
        public ComprasDTO GetCompra(int id)
        {
            ComprasDTO compra = compraRepository.GetCompraByID(id);
            return compra;
        }

        // GET: api/Compras/06
        [ActionName("getCompraByMonth")]
        public IQueryable<ComprasDTO> GetCompraByMonth(int id)
        {
            var compras = compraRepository.GetCompraByMonth(id);
            return compras;
        }

        // PUT: api/Compras/5
        [ActionName("editCompra")]
        [HttpPut]
        public ComprasDTO PutCompra (int id, Compra compraEditada)
        {
            Compra compra = new Compra()
            {
                CompraID = compraEditada.CompraID,
                CategoriaID = compraEditada.CategoriaID,
                EstabelecimentoID = compraEditada.EstabelecimentoID,
                Data = compraEditada.Data,
                Valor = compraEditada.Valor,
                Estabelecimento = compraEditada.Estabelecimento,
                Categoria = compraEditada.Categoria
            };

            compraRepository.UpdateCompra(compra);
            compraRepository.SaveCompra();

            ComprasDTO compraDTO = compraRepository.GetCompraByID(compraEditada.CompraID);

            return compraDTO;
        }

        // POST: api/Compras
        [ResponseType(typeof(Compra))]
        [ActionName("addCompra")]
        public ComprasDTO PostCompra(Compra nova_compra)
        {
            ComprasDTO compraDTO = compraRepository.InsertCompra(nova_compra);

            compraRepository.SaveCompra();
            compraDTO.CompraID = nova_compra.CompraID;

            return compraDTO;
        }

        // DELETE: api/Compras/5
        [ResponseType(typeof(Compra))]
        [ActionName("deleteCompra")]
        public void DeleteCompra(int id)
        {
            /*
            Compra compra = db.Compra.Find(id);
            if (compra == null)
            {
                return new Compra();
            }
            */

            //db.Compra.Remove(compra);

            compraRepository.DeleteCompra(id);
            compraRepository.SaveCompra();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompraExists(int id)
        {
            return context.Compra.Count(e => e.CompraID == id) > 0;
        }
    }
}