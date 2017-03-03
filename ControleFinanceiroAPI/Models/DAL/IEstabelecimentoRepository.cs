using ControleFinanceiroAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleFinanceiroAPI.DAL
{
    public interface IEstabelecimentoRepository : IDisposable
    {
        IQueryable<EstabelecimentosDTO> GetEstabelecimentos();
        EstabelecimentosDTO GetEstabelecimentoByID(int EstabelecimentoID);
        EstabelecimentosDTO InsertEstabelecimento(Estabelecimento estabelecimento);
        void DeleteEstabelecimento(int EstabelecimentoID);
        void UpdateEstabelecimento(Estabelecimento estabelecimento);
        void SaveEstabelecimento();
    }
}