using BusinessRules;
using BusinessRules.Interfaces;
using DTO;
using Infra;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("RestAPIFurb/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ComandasController : ControllerBase
    {
        private readonly IComandaService _service;

        public ComandasController(IComandaService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult BuscarTodos()
        {
            try
            {
                var result = _service.GetAll();
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
        public async Task<IActionResult> Inserir([FromBody]ComandaViewModel Comanda)
        {
            try
            {
                var result = await _service.Insert(CustomAutoMapper<Comanda, ComandaViewModel>.Map(Comanda));
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
        public async Task<IActionResult> Editar(int id, [FromBody]ComandaViewModel Comanda)
        {
            try
            {
                var result = await _service.Update(CustomAutoMapper<Comanda, ComandaViewModel>.Map(Comanda));
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
    }
}