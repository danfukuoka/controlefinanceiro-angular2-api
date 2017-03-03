using ControleFinanceiroAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ControleFinanceiroAPI.DAL
{
    public class EstabelecimentoRepository : IEstabelecimentoRepository, IDisposable
    {
        private ControleFinanceiro1Entities context;

        public EstabelecimentoRepository(ControleFinanceiro1Entities context)
        {
            this.context = context;
        }

        public IQueryable<EstabelecimentosDTO> GetEstabelecimentos()
        {
            var estabelecimentos = from c in context.Estabelecimento
                          select new EstabelecimentosDTO()
                          {
                              EstabelecimentoID = c.EstabelecimentoID,
                              Nome = c.Nome
                          };
            return estabelecimentos;
        }

        public EstabelecimentosDTO GetEstabelecimentoByID(int id)
        {
            EstabelecimentosDTO estabelecimentoSelecionado = context.Estabelecimento.Find(id).toDTO();
            return estabelecimentoSelecionado;
        }

        public EstabelecimentosDTO InsertEstabelecimento(Estabelecimento Estabelecimento)
        {
            Estabelecimento NovoEstabelecimento = context.Estabelecimento.Add(Estabelecimento);

            return NovoEstabelecimento.toDTO();
        }

        public void DeleteEstabelecimento(int EstabelecimentoID)
        {
            Estabelecimento Estabelecimento = context.Estabelecimento.Find(EstabelecimentoID);
            context.Estabelecimento.Remove(Estabelecimento);
        }

        public void UpdateEstabelecimento(Estabelecimento Estabelecimento)
        {
            context.Entry(Estabelecimento).State = EntityState.Modified;
        }

        public void SaveEstabelecimento()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}