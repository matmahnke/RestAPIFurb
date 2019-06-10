using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class AuthUserViewModel
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
