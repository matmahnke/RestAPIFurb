using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO
{
    public class Comanda
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        public virtual Usuario usuario { get; set; }

        public string Produtos { get; set; }

        public decimal ValorTotal { get; set; }
    }
}
