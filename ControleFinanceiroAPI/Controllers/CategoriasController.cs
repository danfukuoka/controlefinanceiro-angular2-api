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
    public class CategoriasController : ApiController
    {
        private ControleFinanceiro1Entities context;
        private ICategoriaRepository categoriaRepository;

        public CategoriasController()
        {
            context = new ControleFinanceiro1Entities();
            this.categoriaRepository = new CategoriaRepository(context);
        }

        // GET: api/Categorias
        public IQueryable<CategoriasDTO> GetCategoria()
        {

            var categorias = categoriaRepository.GetCategorias();

            return categorias;

        }

        // GET: api/Categorias/5
        [ActionName("getCategoria")]
        [ResponseType(typeof(Categoria))]
        public CategoriasDTO GetCategoria(int id)
        {
            CategoriasDTO categoria = categoriaRepository.GetCategoriaByID(id);
            return categoria;
        }

        // PUT: api/Categorias/5
        [ResponseType(typeof(void))]
        [ActionName("editCategoria")]
        [HttpPut]
        public CategoriasDTO PutCategoria(int id, Categoria categoriaEditada)
        {
            Categoria categoria = new Categoria()
            {
                CategoriaID = categoriaEditada.CategoriaID,
                Nome = categoriaEditada.Nome
            };

            categoriaRepository.UpdateCategoria(categoria);
            categoriaRepository.SaveCategoria();

            CategoriasDTO categoriaDTO = categoriaRepository.GetCategoriaByID(categoriaEditada.CategoriaID);

            return categoriaDTO;
        }

        // POST: api/Categorias
        [ResponseType(typeof(Categoria))]
        [ActionName("addCategoria")]
        public CategoriasDTO PostCategoria(Categoria nova_categoria)
        {
            CategoriasDTO categoriaDTO = categoriaRepository.InsertCategoria(nova_categoria);

            categoriaRepository.SaveCategoria();
            categoriaDTO.CategoriaID = nova_categoria.CategoriaID;

            return categoriaDTO;
        }

        // DELETE: api/Categorias/5
        [ResponseType(typeof(Categoria))]
        [ActionName("deleteCategoria")]
        public void DeleteCategoria(int id)
        {
            categoriaRepository.DeleteCategoria(id);
            categoriaRepository.SaveCategoria();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoriaExists(int id)
        {
            return context.Categoria.Count(e => e.CategoriaID == id) > 0;
        }
    }
}