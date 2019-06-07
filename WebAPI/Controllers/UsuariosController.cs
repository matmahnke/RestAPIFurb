using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BusinessRules;
using BusinessRules.Interfaces;
using DTO;
using Infra;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("RestAPIFurb/[controller]")]
    [ApiController]
    [Authorize()]
    public class UsuariosController : ControllerBase
    {
        private IUserService _userService;
        private readonly IUsuarioService _service;

        public UsuariosController(IUserService userService, IUsuarioService service)
        {
            _userService = userService;
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<IActionResult> Authenticate([FromBody]UsuarioViewModel userParam)
        {
            var user = await _userService.Authenticate(userParam.Email, userParam.Senha);

            if (user == null)
                return BadRequest(new { message = "Usuário ou senha estão incorretos" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            try
            {
                var result = _service.GetAll();
                return Ok(result);
            }
            catch(BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            try
            {
                var result = _service.GetById(id);
                return Ok(result);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Inserir([FromBody]UsuarioViewModel usuario)
        {
            try
            {
                var result = await _service.Insert(CustomAutoMapper<Usuario, UsuarioViewModel>.Map(usuario));
                return Ok(result);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody]UsuarioViewModel usuario)
        {
            try
            {
                var result = await _service.Update(CustomAutoMapper<Usuario, UsuarioViewModel>.Map(usuario));
                return Ok(result);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                var result = _service.Delete(id);
                return Ok(result);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{email}")]
        public async Task<IActionResult> Deletar(string email)
        {
            try
            {
                var result = _service.Delete(email);
                return Ok(result);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}