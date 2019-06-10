using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebAPI;
using WebAPI.Models;
using WebAPI.Models.Usuarios;
using Xunit;

namespace FurbAPI.Integration.tests
{
    public class UsuariosResponseFormatTest
    : IClassFixture<CustomFactory<TestStartup>>
    {
        private readonly WebApplicationFactory<TestStartup> _factory;


        public UsuariosResponseFormatTest(CustomFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/RestAPIFurb/Usuarios")]
        public async Task Post_Usuario(string url)
        {
            var client = _factory.CreateClient();

            var response = new UsuarioRules(client, url).Criar("criando@exemplo.com", "exemplo123");

        }

        [Theory]
        [InlineData("/RestAPIFurb/Usuarios")]
        public async Task Auth_Usuario(string url)
        {
            var client = _factory.CreateClient();
            var rule = new UsuarioRules(client, url);
            var usuario = rule.Criar("autenticando@exemplo.com", "exemplo123");
            var token = await rule.Autenticar(usuario.Result);
        }

        [Theory]
        [InlineData("/RestAPIFurb/Usuarios")]
        public async Task Put_Usuario(string url)
        {
            var client = _factory.CreateClient();
            var rule = new UsuarioRules(client, url);
            var usuario = await rule.Criar("alterando@exemplo.com", "exemplo123");
            var token = await rule.Autenticar(usuario);

            var usuarioAlterado = rule.Alterar(usuario.Id.Value, "alterando2@exemplo.com", "exemplo321");
        }

        [Theory]
        [InlineData("/RestAPIFurb/Usuarios")]
        public async Task Get_Usuario(string url)
        {
            var client = _factory.CreateClient();
            var rule = new UsuarioRules(client, url);
            var usuario = await rule.Criar("buscandoPorId@exemplo.com", "exemplo123");
            var token = await rule.Autenticar(usuario);
            var user = await rule.BuscarPorId(usuario);
        }

        [Theory]
        [InlineData("/RestAPIFurb/Usuarios")]
        public async Task Get_Usuarios(string url)
        {
            var client = _factory.CreateClient();
            var rule = new UsuarioRules(client, url);
            var usuario1 = await rule.Criar("buscandoTodos1@exemplo.com", "exemplo123");
            var usuario2 = await rule.Criar("buscandoTodos2@exemplo.com", "exemplo123");
            var token = await rule.Autenticar(usuario1);
            var user = await rule.BuscarTodos(new List<UsuarioViewModel> { usuario1, usuario2});
        }
        [Theory]
        [InlineData("/RestAPIFurb/Usuarios")]
        public async Task Delete_Usuario_By_Id(string url)
        {
            var client = _factory.CreateClient();
            var rule = new UsuarioRules(client, url);
            var usuario = await rule.Criar("deletarPorId@exemplo.com", "exemplo123");
            var token = await rule.Autenticar(usuario);
            await rule.DeletarPorId(usuario.Id.Value);
            var response = await client.GetAsync(url+$"/{usuario.Id}");
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
            Assert.True(await response.Content.ReadAsStringAsync() == "");
        }
        [Theory]
        [InlineData("/RestAPIFurb/Usuarios")]
        public async Task Delete_Usuario_By_Email(string url)
        {
            var client = _factory.CreateClient();
            var rule = new UsuarioRules(client, url);
            var usuario = await rule.Criar("deletarPorEmail@exemplo.com", "exemplo123");
            var token = await rule.Autenticar(usuario);
            await rule.DeletarPorId(usuario.Id.Value);
            var response = await client.GetAsync(url+$"/{usuario.Id}");
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
            Assert.True(await response.Content.ReadAsStringAsync() == "");
        }
    }
}
