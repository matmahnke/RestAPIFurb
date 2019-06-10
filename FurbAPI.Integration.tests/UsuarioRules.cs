using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.Usuarios;
using Xunit;

namespace FurbAPI.Integration.tests
{
    public class UsuarioRules
    {
        public UsuarioRules(HttpClient client, string url)
        {
            _client = client;
            _url = url;
        }

        private readonly HttpClient _client;

        private readonly string _url;

        public async Task<UsuarioViewModel> Criar(string email, string senha)
        {
            var content = new InfoUsuarioViewModel
            {
                Email = email,
                Senha = senha
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(_url, httpContent);

            Assert.True(System.Net.HttpStatusCode.OK == response.StatusCode);


            var result = JsonConvert.DeserializeObject<UsuarioViewModel>(await response.Content.ReadAsStringAsync());

            Assert.True(content.Email == result.Email);
            Assert.True(content.Senha == result.Senha);
            Assert.True(result.Id > 0);

            return result;
        }


        public async Task<string> Autenticar(UsuarioViewModel usuario)
        {
            var content = new InfoUsuarioViewModel
            {
                Email = usuario.Email,
                Senha = usuario.Senha
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(_url + $"/auth", httpContent);

            Assert.True(System.Net.HttpStatusCode.OK == response.StatusCode);


            var result = JsonConvert.DeserializeObject<AuthUserViewModel>(await response.Content.ReadAsStringAsync());

            Assert.True(content.Email == result.Email);
            Assert.True(!string.IsNullOrWhiteSpace(result.Token));
            Assert.True(result.Id > 0);

            // seta o token no header no client por referencia no heap
            _client.DefaultRequestHeaders.Authorization
                                     = new AuthenticationHeaderValue("Bearer", result.Token);

            return result.Token;
        }

        public async Task<List<UsuarioViewModel>> BuscarTodos()
        {
            var response = await _client.GetAsync(_url);
            Assert.True(System.Net.HttpStatusCode.OK == response.StatusCode);
            var usuarios = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(await response.Content.ReadAsStringAsync());
            return usuarios;
        }

        public async Task<UsuarioViewModel> Alterar(int id, string email, string senha)
        {
            var content = new InfoUsuarioViewModel
            {
                Email = email,
                Senha = senha
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(_url + $"/{id}", httpContent);

            Assert.True(System.Net.HttpStatusCode.OK == response.StatusCode);


            var result = JsonConvert.DeserializeObject<UsuarioViewModel>(await response.Content.ReadAsStringAsync());

            Assert.True(content.Email == result.Email);
            Assert.True(content.Senha == result.Senha);
            Assert.True(result.Id > 0);

            return result;
        }

        public async Task<UsuarioViewModel> BuscarPorId(UsuarioViewModel usuario)
        {
            var response = await _client.GetAsync(_url + $"/{usuario.Id}");
            var result = JsonConvert.DeserializeObject<UsuarioViewModel>(await response.Content.ReadAsStringAsync());

            Assert.True(result.Id == usuario.Id);
            Assert.True(result.Email == usuario.Email);
            Assert.True(result.Senha == usuario.Senha);
            return result;
        }

        public async Task<List<UsuarioViewModel>> BuscarTodos(List<UsuarioViewModel> usuarios)
        {
            var response = await _client.GetAsync(_url);
            var result = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(await response.Content.ReadAsStringAsync());

            foreach (var usuario in usuarios)
            {
                Assert.True(result.Any(u => u.Email == usuario.Email
                && u.Senha == usuario.Senha));
            }
            return result;
        }
        public async Task DeletarPorId(int id)
        {
            var response = await _client.DeleteAsync(_url + $"/{id}");
            var result = await response.Content.ReadAsStringAsync();
            Assert.True("{\"success\":{\"text\":\"usuário removido\"}}" == result);
        }
        public async Task DeletarPorEmail(string email)
        {
            var content = new StringContent("{\"email\":\"" + email + "\"}", Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Content = content,
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_client.BaseAddress + _url)
            };

            var response = await _client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            Assert.True("{\"success\":{\"text\":\"usuário removido\"}}" == result);
        }
    }
}
