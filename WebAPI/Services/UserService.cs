using BusinessRules.Interfaces;
using Infra;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IUserService
    {
        Task<AuthUserViewModel> Authenticate(string email, string senha);
        Task<IEnumerable<UsuarioViewModel>> GetAll();
    }

    public class UserService : IUserService
    {
        private readonly IUsuarioService _service;
        private readonly IConfiguration _configuration;

        public UserService(IUsuarioService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        public async Task<AuthUserViewModel> Authenticate(string email, string senha)
        {
            var result = _service.Filter(email, senha);

            // return null if user not found
            if (result == null)
                return null;

            var user = new AuthUserViewModel
            {
                Email = result.Email,
                Id = result.Id
            };

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();

            var appSettingsSection = _configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

            };

            try
            {
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {

                throw;
            }

            return user;
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
