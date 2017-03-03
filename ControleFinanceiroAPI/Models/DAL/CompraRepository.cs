using ControleFinanceiroAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace ControleFinanceiroAPI.DAL
{
    public class CompraRepository : ICompraRepository, IDisposable
    {
        private ControleFinanceiro1Entities context;

        public CompraRepository(ControleFinanceiro1Entities context)
        {
            this.context = context;
        }

        public IQueryable<ComprasDTO> GetCompras()
        {
            var compras = from c in context.Compra
                          select new ComprasDTO()
                          {
                              CompraID = c.CompraID,
                              CategoriaID = c.Categoria.CategoriaID,
                              CategoriaNome = c.Categoria.Nome,
                              Valor = c.Valor,
                              EstabelecimentoID = c.Estabelecimento.EstabelecimentoID,
                              EstabelecimentoNome = c.Estabelecimento.Nome,
                              Data = c.Data
                          };
            return compras;
        }

        public ComprasDTO GetCompraByID(int id)
        {
            ComprasDTO compraSelecionada = context.Compra.Find(id).toDTO();
            return compraSelecionada;
        }

        public IQueryable<ComprasDTO> GetCompraByMonth(int month)
        {
            var compras = from c in context.Compra where c.Data.Month == month
                          select new ComprasDTO()
                          {
                              CompraID = c.CompraID,
                              CategoriaID = c.Categoria.CategoriaID,
                              CategoriaNome = c.Categoria.Nome,
                              Valor = c.Valor,
                              EstabelecimentoID = c.Estabelecimento.EstabelecimentoID,
                              EstabelecimentoNome = c.Estabelecimento.Nome,
                              Data = c.Data
                          };
            return compras;
        }

        public ComprasDTO InsertCompra(Compra compra)
        {
            compra.Estabelecimento = context.Estabelecimento.First(x => x.EstabelecimentoID == compra.EstabelecimentoID);
            compra.Categoria = context.Categoria.First(x => x.CategoriaID == compra.CategoriaID);
            Compra novaCompra = context.Compra.Add(compra);

            return novaCompra.toDTO();

        }

        public void DeleteCompra(int CompraID)
        {
            Compra Compra = context.Compra.Find(CompraID);
            Compra CompraDeletada = context.Compra.Remove(Compra);

        }

        public void UpdateCompra(Compra Compra)
        {
            context.Entry(Compra).State = EntityState.Modified;
        }

        public void SaveCompra()
        {
            try
            {
                context.SaveChanges();

            }
            catch(DbEntityValidationException ex)
            {
                foreach (var failure in ex.EntityValidationErrors)
                {
                    string validationErrors = "";

                    foreach (var error in failure.ValidationErrors)
                    {
                        validationErrors += error.PropertyName + "  " + error.ErrorMessage;
                    }
                }
            }
            
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