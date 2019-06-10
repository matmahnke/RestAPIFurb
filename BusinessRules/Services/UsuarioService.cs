using BusinessRules.Interfaces;
using BusinessRules.Utils;
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

        private readonly IApplicationDbContext _service;

        public UsuarioService(IApplicationDbContext service)
        {
            _service = service;
        }
        public override void Validate(Usuario item)
        {
            emailValido(item.Email);
            senhaValida(item.Senha);

            base.Validate(item);
        }

        private void emailValido(string email)
        {

            if (string.IsNullOrWhiteSpace(email))
            {
                base.AddError(new ErrorField() { Message = "O campo Email é obrigatório!", Field = "Email" });
            }
            else if (!email.IsValidEmail())
            {
                base.AddError(new ErrorField() { Message = "Email inválido!", Field = "Email" });
            }
        }

        private void senhaValida(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
            {
                base.AddError(new ErrorField() { Message = "O campo Senha é obrigatório!", Field = "Senha" });
            }
        }

        public async Task Delete(int id)
        {
            using (var context = _service)
            {
                var user = GetById(id);
                if (user == null)
                    base.AddError(new ErrorField() { Message = "Usuário não encontrado!", Field = "Id" });
                context.Usuarios.Remove(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(string email)
        {
            using (var context = _service)
            {
                var usuario = context.Usuarios.FirstOrDefault(c => c.Email == email);
                if (usuario == null)
                    base.AddError(new ErrorField() { Message = "Usuário não encontrado!", Field = "Email" });
                var result = context.Usuarios.Remove(await context.Usuarios.
                    FirstOrDefaultAsync(c => c.Email == email));
                await context.SaveChangesAsync();
            }
        }

        public IList<Usuario> GetAll()
        {
            using (var context = _service)
            {
                var result = context.Usuarios.AsNoTracking().ToList();
                return result;
            }
        }

        public Usuario GetById(int id)
        {
            var result = _service.Usuarios.FirstOrDefault(c => c.Id == id);
            return result;
        }

        public Usuario Filter(string email, string senha)
        {
            using (var context = _service)
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    base.AddError(new ErrorField() { Message = "Email obrigatório!", Field = "Email" });
                }
                if (string.IsNullOrWhiteSpace(senha))
                {
                    base.AddError(new ErrorField() { Message = "O campo Senha é obrigatório!", Field = "Senha" });
                }
                var result = context.Usuarios.FirstOrDefault(c => c.Email == email && c.Senha == senha);
                return result;
            }
        }

        public async Task<Usuario> Insert(Usuario usuario)
        {
            Validate(usuario);
            Usuario result;
            using (var context = _service)
            {
                result = (await context.Usuarios.AddAsync(usuario)).Entity;
                await context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<Usuario> Update(Usuario usuario)
        {
            var user = GetById(usuario.Id);

            if (!string.IsNullOrEmpty(usuario.Email))
            {
                emailValido(usuario.Email);
                user.Email = usuario.Email;
            }

            if (!string.IsNullOrEmpty(usuario.Senha))
            {
                senhaValida(usuario.Senha);
                user.Senha = usuario.Senha;
            }

            using (var context = _service)
            {
                if (user == null)
                    base.AddError(new ErrorField() { Message = "Usuário não encontrado!", Field = "Id" });

                var result = context.Usuarios.Update(user);
                await context.SaveChangesAsync();
                return result.Entity;
            }
        }
    }
}
