using DTO;
using Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("RestAPIFurb/[controller]")]
    [ApiController]
    public class ComandasController : ControllerBase
    {
        [HttpGet]
        public IActionResult BuscarTodos()
        {
            using (var context = new ApplicationDbContext())
            {
                return Ok(context.Comandas.AsNoTracking().ToList());
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return Ok(await context.Comandas.FirstOrDefaultAsync(u => u.Id == id));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]ComandaViewModel comanda)
        {
            using (var context = new ApplicationDbContext())
            {
                var result = await context.Comandas.AddAsync(new Comanda
                {
                    IdUsuario = comanda.IdUsuario,
                    Produtos = comanda.Produtos,
                    ValorTotal = comanda.ValorTotal
                });
                await context.SaveChangesAsync();
                return Ok(result);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody]ComandaViewModel comanda)
        {
            using (var context = new ApplicationDbContext())
            {
                var result = context.Comandas.Update(new Comanda
                {
                    Id = comanda.Id.Value,
                    IdUsuario = comanda.IdUsuario,
                    Produtos = comanda.Produtos,
                    ValorTotal = comanda.ValorTotal
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
                var comanda = context.Comandas.Remove(await context.Comandas.FirstAsync(c => c.Id == id));
                await context.SaveChangesAsync();
                return Ok(comanda);
            }
        }
    }
}