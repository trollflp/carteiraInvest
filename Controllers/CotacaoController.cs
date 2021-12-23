using CarteiraSafra.Data;
using CarteiraSafra.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarteiraSafra.Controllers
{
    [Route("v1")]
    [ApiController]
    public class CotacaoController : ControllerBase
    {
        /// <summary>
        /// Retorna cotação em tempo real de uma ação
        /// </summary>
        /// <param name="symbol">Enviar o código/símbolo da ação (Exemplos: ALLK, TSLA, NAKD, PFE..)</param>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Ação não encontrada ou faltando informações</response>
        /// <response code="400">Erro ao buscar a ação</response>
        [HttpGet]
        [Route("cotacao/{symbol}")]
        public async Task<IActionResult> GetById(
            [FromServices] AppDbContext context, 
            [FromRoute] string symbol)
        {
            if (String.IsNullOrEmpty(symbol)) return BadRequest();

            Cotacao cotacao = new Cotacao();

            try
            {
                cotacao = Util.GetCotacaoBySymbol.retornaCotacao(symbol.Trim().ToUpper());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return cotacao == null ? NotFound() : Ok(cotacao);
        }
    }
}
