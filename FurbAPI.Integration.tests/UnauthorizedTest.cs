using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebAPI;
using WebAPI.Models.Usuarios;
using Xunit;

namespace FurbAPI.Integration.tests
{

    public class UnauthorizedTests
    : IClassFixture<CustomFactory<TestStartup>>
    {
        private readonly WebApplicationFactory<TestStartup> _factory;

        public UnauthorizedTests(CustomFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/RestAPIFurb/Usuarios")]
        public async Task Get_Usuarios_Return_Unauthorized(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData("/RestAPIFurb/Usuarios/1")]
        public async Task Get_ById_Usuarios_Return_Unauthorized(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData("/RestAPIFurb/Usuarios/1")]
        public async Task Put_Usuarios_Return_Unauthorized(string url)
        {
            var client = _factory.CreateClient();

            var httpContent = new StringContent("{}", Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, httpContent);

            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData("/RestAPIFurb/Usuarios/1")]
        public async Task Delete_Usuarios_Return_Unauthorized(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync(url);

            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData("RestAPIFurb/Usuarios")]
        public async Task Delete_ByEmail_Usuarios_Return_Unauthorized(string url)
        {
            var client = _factory.CreateClient();

            var content = new StringContent("{\"email\":\"exemplo@exemplos\"}", Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Content = content,
                Method = HttpMethod.Delete,
                RequestUri = new Uri(client.BaseAddress + url)
            };

            var response = await client.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData("/RestAPIFurb/Comandas")]
        public async Task Get_Comandas_Return_Unauthorized(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData("/RestAPIFurb/Comandas/1")]
        public async Task Get_ById_Comandas_Return_Unauthorized(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData("/RestAPIFurb/Comandas/1")]
        public async Task Put_Comandas_Return_Unauthorized(string url)
        {
            var client = _factory.CreateClient();

            var httpContent = new StringContent("{}", Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, httpContent);

            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData("/RestAPIFurb/Comandas/1")]
        public async Task Delete_Comandas_Return_Unauthorized(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync(url);

            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
