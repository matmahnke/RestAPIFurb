using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ComandaViewModel
    {
        public int? Id { get; set; }
        public int IdUsuario { get; set; }
        public string Produtos { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
