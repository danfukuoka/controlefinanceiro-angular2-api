using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleFinanceiroAPI.Models
{
    public class ComprasDTO
    {
        public int CompraID { get; set; }
        public int CategoriaID { get; set; }
        public string CategoriaNome { get; set; }
        public int EstabelecimentoID { get; set; }

        public string EstabelecimentoNome { get; set; }
        public System.DateTime Data { get; set; }
        public float Valor { get; set; }

    }
}

