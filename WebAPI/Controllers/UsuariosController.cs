using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("RestAPIFurb/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            using (var context = new ApplicationDbContext())
            {
                return Ok(context.Usuarios.AsNoTracking().ToList());
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return Ok(await context.Usuarios.FirstOrDefaultAsync(u => u.Id == id));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]UsuarioViewModel usuario)
        {
            using (var context = new ApplicationDbContext())
            {
                var result = await context.Usuarios.AddAsync(new DTO.Usuario
                {
                    Email = usuario.Email,
                    Senha = usuario.Senha
                });
                await context.SaveChangesAsync();
                return Ok(result);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody]UsuarioViewModel usuario)
        {
            using (var context = new ApplicationDbContext())
            {
                var result = context.Usuarios.Update(new DTO.Usuario
                {
                    Id = usuario.Id.Value,
                    Email = usuario.Email,
                    Senha = usuario.Senha
                });
                await context.SaveChangesAsync();
                return Ok(result);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var result = context.Usuarios.Remove(await context.Usuarios.FirstAsync(c => c.Id == id));
                await context.SaveChangesAsync();
                return Ok(result);
            }
        }

        [HttpDelete]
        [Route("{email}")]
        public async Task<IActionResult> Deletar(string email)
        {
            using (var context = new ApplicationDbContext())
            {
                var result = context.Usuarios.Remove(await context.Usuarios.FirstAsync(c => c.Email == email));
                await context.SaveChangesAsync();
                return Ok(result);
            }
        }
    }
}