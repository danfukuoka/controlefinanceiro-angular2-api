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
    public class EstabelecimentosController : ApiController
    {
        private ControleFinanceiro1Entities context;

        private IEstabelecimentoRepository estabelecimentoRepository;
        //private FinanceiroContext context;

        public EstabelecimentosController()
        {
            context = new ControleFinanceiro1Entities();
            this.estabelecimentoRepository = new EstabelecimentoRepository(context);
        }

        // GET: api/Estabelecimentoes
        public IQueryable<EstabelecimentosDTO> GetEstabelecimento()
        {
            var estabelecimentos = estabelecimentoRepository.GetEstabelecimentos();

            return estabelecimentos;
        }

        // GET: api/Estabelecimentoes/5
        [ActionName("getEstabelecimento")]
        [ResponseType(typeof(Estabelecimento))]
        public EstabelecimentosDTO GetEstabelecimento(int id)
        {
            //Estabelecimento estabelecimento = await db.Estabelecimento.FindAsync(id);

            EstabelecimentosDTO estabelecimento = estabelecimentoRepository.GetEstabelecimentoByID(id);
            return estabelecimento;
        }

        // PUT: api/Estabelecimentoes/5
        [ResponseType(typeof(void))]
        [ActionName("editEstabelecimento")]
        [HttpPut]
        public EstabelecimentosDTO PutEstabelecimento(int id, Estabelecimento estabelecimentoEditado)
        {
            Estabelecimento estabelecimento = new Estabelecimento()
            {
                EstabelecimentoID = estabelecimentoEditado.EstabelecimentoID,
                Nome = estabelecimentoEditado.Nome
            };

            estabelecimentoRepository.UpdateEstabelecimento(estabelecimento);
            estabelecimentoRepository.SaveEstabelecimento();

            EstabelecimentosDTO estabelecimentoDTO = estabelecimentoRepository.GetEstabelecimentoByID(estabelecimentoEditado.EstabelecimentoID);

            return estabelecimentoDTO;
        }

        // POST: api/Estabelecimentoes
        [ResponseType(typeof(Estabelecimento))]
        [ActionName("addEstabelecimento")]
        public EstabelecimentosDTO PostEstabelecimento(Estabelecimento novo_estabelecimento)
        {
            EstabelecimentosDTO estabelecimentoDTO = estabelecimentoRepository.InsertEstabelecimento(novo_estabelecimento);

            estabelecimentoRepository.SaveEstabelecimento();
            estabelecimentoDTO.EstabelecimentoID = novo_estabelecimento.EstabelecimentoID;

            return estabelecimentoDTO;
        }

        // DELETE: api/Estabelecimentoes/5
        [ResponseType(typeof(Estabelecimento))]
        [ActionName("deleteEstabelecimento")]
        public void DeleteEstabelecimento(int id)
        {
            estabelecimentoRepository.DeleteEstabelecimento(id);
            estabelecimentoRepository.SaveEstabelecimento();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EstabelecimentoExists(int id)
        {
            return context.Estabelecimento.Count(e => e.EstabelecimentoID == id) > 0;
        }
    }
}