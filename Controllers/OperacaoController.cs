using CarteiraSafra.Data;
using CarteiraSafra.Models;
using CarteiraSafra.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarteiraSafra.Controllers
{
    [Route("v1")]
    [ApiController]
    public class OperacaoController : ControllerBase
    {
        /// <summary>
        /// Cria uma nova operação
        /// </summary>
        /// <param name="model">Informar o codigo/simbolo da ação (codigoAcao), a compra (C) ou venda (V) da ação em tipoOperacao e a quantidade (quantidadeOperacao)</param>
        /// <response code="201">Operação registrada</response>
        /// <response code="400">Erro ao criar a operação</response>
        [HttpPost("operacoes")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context, 
            [FromBody] OperarAcaoViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Cotacao cotacao = new Cotacao();

            try
            {
                cotacao = Util.GetCotacaoBySymbol.retornaCotacao(model.CodigoAcao);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            double ValorAcao = model.TipoOperacao == "C" ? cotacao.PrecoCompra : cotacao.PrecoVenda;

            var operacao = new Operacao
            {
                CodigoAcao = model.CodigoAcao,
                RazaoSocial = cotacao.RazaoSocial,
                TipoOperacao = model.TipoOperacao,
                Date = DateTime.Now,
                QuantidadeOperacao = model.QuantidadeOperacao,
                ValorAcao = ValorAcao,
                ValorTotalOperacao = ((model.QuantidadeOperacao * ValorAcao) + 5) + (((model.QuantidadeOperacao * ValorAcao) * 0.0325) / 100)
            };                        

            try
            {
                await context.Operacoes.AddAsync(operacao);
                await context.SaveChangesAsync();
                return Created($"v1/acoes/{operacao.CodigoAcao}", operacao);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Lista o relatório de operações
        /// </summary>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [Route("operacoes")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context)
        {
            var operacoes = await context.Operacoes.AsNoTracking().ToListAsync();
            return Ok(operacoes);
        }


        /// <summary>
        /// Detalha uma operação
        /// </summary>
        /// <param name="id">Enviar o ID da operação</param>
        /// <response code="200">Sucesso</response>
        /// <response code="404">ID não encontrado</response>
        [HttpGet]
        [Route("operacoes/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var operacoes = await context.Operacoes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return operacoes == null ? NotFound() : Ok(operacoes);
        }
    }
}
