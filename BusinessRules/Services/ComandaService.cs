using BusinessRules.Interfaces;
using DTO;
using Infra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.Services
{
    public class ComandaService : BaseValidator<Comanda>, IComandaService
    {

        public override void Validate(Comanda item)
        {
            if (string.IsNullOrWhiteSpace(item.Produtos))
            {
                base.AddError(new ErrorField() { Message = "O campo valor produtos é obrigatório!", Field = "produtos" });
            }
            if (item.IdUsuario == 0)
            {
                base.AddError(new ErrorField() { Message = "O usuário é obrigatório!", Field = "idUsuario" });
            }else 
            if(new UsuarioService().GetById(item.IdUsuario) == null)
            {
                base.AddError(new ErrorField() { Message = "Usuário inválido!", Field = "idUsuario" });
            }
            base.Validate(item);
        }

        public async Task Delete(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var result = context.Comandas.Remove(GetById(id));
                await context.SaveChangesAsync();
            }
        }

        public IList<Comanda> GetAll()
        {
            using (var context = new ApplicationDbContext())
            {
                var result = context.Comandas.AsNoTracking().ToList();
                return result;
            }
        }

        public Comanda GetById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var result = context.Comandas.FirstOrDefault(c => c.Id == id);
                return result;
            }
        }

        public async Task<Comanda> Insert(Comanda comanda)
        {
            Validate(comanda);
            using (var context = new ApplicationDbContext())
            {
                var result = await context.Comandas.AddAsync(comanda);
                await context.SaveChangesAsync();
                return result.Entity;
            }
        }

        public async Task<Comanda> Update(Comanda comanda)
        {
            Validate(comanda);
            using (var context = new ApplicationDbContext())
            {
                var result = context.Comandas.Update(comanda);
                await context.SaveChangesAsync();
                return result.Entity;
            }
        }
    }
}
