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
    public class UsuarioService : BaseValidator<Usuario>, IUsuarioService
    {
        public override void Validate(Usuario item)
        {
            if (string.IsNullOrWhiteSpace(item.Email))
            {
                base.AddError(new ErrorField() { Message = "O campo Senha é obrigatório!", Field = "Email" });
            }
            if (string.IsNullOrWhiteSpace(item.Email))
            {
                base.AddError(new ErrorField() { Message = "O campo Senha é obrigatório!", Field = "Senha" });
            }
            base.Validate(item);
        }
        public async Task Delete(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var result = context.Usuarios.Remove(GetById(id));
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(string email)
        {
            using (var context = new ApplicationDbContext())
            {
                var result = context.Usuarios.Remove(await context.Usuarios.
                    FirstOrDefaultAsync(c => c.Email == email));
                await context.SaveChangesAsync();
            }
        }

        public IList<Usuario> GetAll()
        {
            using (var context = new ApplicationDbContext())
            {
                var result = context.Usuarios.AsNoTracking().ToList();
                return result;
            }
        }

        public Usuario GetById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var result = context.Usuarios.FirstOrDefault(c => c.Id == id);
                return result;
            }
        }

        public Usuario Filter(string email, string senha)
        {
            using (var context = new ApplicationDbContext())
            {
                var result = context.Usuarios.FirstOrDefault(c => c.Email == email && c.Senha == senha);
                return result;
            }
        }

        public async Task<Usuario> Insert(Usuario usuario)
        {
            Validate(usuario);
            using (var context = new ApplicationDbContext())
            {
                var result = await context.Usuarios.AddAsync(usuario);
                await context.SaveChangesAsync();
                return result.Entity;
            }
        }

        public async Task<Usuario> Update(Usuario usuario)
        {
            Validate(usuario);
            using (var context = new ApplicationDbContext())
            {
                var result = context.Usuarios.Update(usuario);
                await context.SaveChangesAsync();
                return result.Entity;
            }
        }
    }
}
