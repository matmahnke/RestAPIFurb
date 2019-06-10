using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Comandas
{
    public class InfoComandaViewModel
    {
        public int IdUsuario { get; set; }
        public string Produtos { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
