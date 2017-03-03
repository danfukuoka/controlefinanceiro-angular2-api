using ControleFinanceiroAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ControleFinanceiroAPI.DAL
{
    public class CategoriaRepository : ICategoriaRepository, IDisposable
    {
        private ControleFinanceiro1Entities context;

        public CategoriaRepository(ControleFinanceiro1Entities context)
        {
            this.context = context;
        }

        public IQueryable<CategoriasDTO> GetCategorias()
        {
            var categorias = from c in context.Categoria
                          select new CategoriasDTO()
                          {
                              CategoriaID = c.CategoriaID,
                              Nome = c.Nome
                          };
            return categorias;
        }

        public CategoriasDTO GetCategoriaByID(int id)
        {
            CategoriasDTO categoriaSelecionada = context.Categoria.Find(id).toDTO();
            return categoriaSelecionada;
        }

        public CategoriasDTO InsertCategoria(Categoria Categoria)
        {
            Categoria NovaCompra = context.Categoria.Add(Categoria);

            return NovaCompra.toDTO();
        }

        public void DeleteCategoria(int CategoriaID)
        {
            Categoria Categoria = context.Categoria.Find(CategoriaID);
            Categoria CompraDeletada = context.Categoria.Remove(Categoria);
        }

        public void UpdateCategoria(Categoria Categoria)
        {
            context.Entry(Categoria).State = EntityState.Modified;
        }

        public void SaveCategoria()
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