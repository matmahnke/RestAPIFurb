using Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IUserService
    {
        Task<UsuarioViewModel> Authenticate(string email, string senha);
        Task<IEnumerable<UsuarioViewModel>> GetAll();
    }

    public class UserService : IUserService
    {
        public async Task<UsuarioViewModel> Authenticate(string email, string senha)
        {
            using (var context = new ApplicationDbContext())
            {
                var user = await Task.Run(() => context.Usuarios.FirstOrDefault(c => c.Email == email && c.Senha == senha));

                if (user == null)
                    return null;

                return new UsuarioViewModel
                {
                    Id = user.Id,
                    Email = user.Email
                };
            }
        }

        public async Task<IEnumerable<UsuarioViewModel>> GetAll()
        {
            using (var context = new ApplicationDbContext())
            {
                return await Task.Run(() => context.Usuarios.Select(c => new UsuarioViewModel
                {
                    Id = c.Id,
                    Email = c.Email
                }));
            }
        }
    }
}
