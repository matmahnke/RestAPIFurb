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
        private readonly IApplicationDbContext _service;

        public ComandaService(IApplicationDbContext service)
        {
            _service = service;
        }

        public override void Validate(Comanda item)
        {
            validarUsuario(item.IdUsuario);
            validarProdutos(item.Produtos);
            validarValorTotal(item.ValorTotal);
            base.Validate(item);
        }

        void validarValorTotal(decimal valorTotal)
        {
            if (valorTotal < 0)
            {
                base.AddError(new ErrorField() { Message = "O campo valor total não pode ser negativo!", Field = "valorTotal" });
            }
        }

        void validarProdutos(string produtos)
        {
            if (string.IsNullOrWhiteSpace(produtos))
            {
                base.AddError(new ErrorField() { Message = "O campo valor produtos é obrigatório!", Field = "produtos" });
            }
        }

        void validarUsuario(int id)
        {
            if (id <= 0)
            {
                base.AddError(new ErrorField() { Message = "O usuário é obrigatório!", Field = "idUsuario" });
            }
            else
            if (GetById(id) == null)
            {
                base.AddError(new ErrorField() { Message = "Usuário inválido!", Field = "idUsuario" });
            }
        }

        public async Task Delete(int id)
        {
            using (var context = _service)
            {
                var comanda = GetById(id);
                if (comanda != null)
                {
                    context.Comandas.Attach(comanda);
                    context.Comandas.Remove(comanda);
                    await context.SaveChangesAsync();
                }
            }
        }

        public IList<Comanda> GetAll()
        {
            using (var context = _service)
            {
                var result = context.Comandas.AsNoTracking().ToList();
                return result;
            }
        }

        public Comanda GetById(int id)
        {
            var result = _service.Comandas.FirstOrDefault(c => c.Id == id);
            return result;
        }

        public async Task<Comanda> Insert(Comanda comanda)
        {
            Validate(comanda);
            using (var context = _service)
            {
                var result = await context.Comandas.AddAsync(comanda);
                await context.SaveChangesAsync();
                return result.Entity;
            }
        }

        public async Task<Comanda> Update(Comanda comanda)
        {
            var entidade = GetById(comanda.Id);

            if(comanda.IdUsuario != 0)
            {
                validarUsuario(comanda.IdUsuario);
                entidade.IdUsuario = comanda.IdUsuario;
            }

            if (!string.IsNullOrEmpty(comanda.Produtos))
            {
                validarProdutos(comanda.Produtos);
                entidade.Produtos = comanda.Produtos;
            }

            if(comanda.ValorTotal != 0)
            {
                validarValorTotal(comanda.ValorTotal);
                entidade.ValorTotal = comanda.ValorTotal;
            }

            using (var context = _service)
            {
                var result = context.Comandas.Update(entidade);
                await context.SaveChangesAsync();
                return result.Entity;
            }
        }
    }
}
