using ControleFinanceiroAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleFinanceiroAPI.DAL
{
    public interface ICategoriaRepository : IDisposable
    {
        IQueryable<CategoriasDTO> GetCategorias();
        CategoriasDTO GetCategoriaByID(int CategoriaID);
        CategoriasDTO InsertCategoria(Categoria categoria);
        void DeleteCategoria(int CategoriaID);
        void UpdateCategoria(Categoria categoria);
        void SaveCategoria();
    }
}