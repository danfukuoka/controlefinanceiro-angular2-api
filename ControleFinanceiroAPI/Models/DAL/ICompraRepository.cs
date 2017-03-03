using ControleFinanceiroAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleFinanceiroAPI.DAL
{
    public interface ICompraRepository : IDisposable
    {
        IQueryable<ComprasDTO> GetCompras();
        ComprasDTO GetCompraByID(int CompraID);
        ComprasDTO InsertCompra(Compra compra);
        void DeleteCompra(int CompraID);
        void UpdateCompra(Compra compra);
        void SaveCompra();
        IQueryable<ComprasDTO> GetCompraByMonth(int month);
    }
}