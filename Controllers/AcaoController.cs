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
    public class AcaoController : ControllerBase
    {
        /// <summary>
        /// Lista ações
        /// </summary>        
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [Route("acoes")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context)
        {
            var acoes = await context.Acoes.AsNoTracking().ToListAsync();
            return Ok(acoes);
        }

        /// <summary>
        /// Detalha uma ação
        /// </summary>
        /// <param name="id">ID da ação registrada</param>        
        /// <response code="200">Ação encontrada</response>
        /// <response code="404">Ação não encontrada</response>

        [HttpGet]
        [Route("acoes/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] AppDbContext context, 
            [FromRoute] int id)
        {
            var acao = await context.Acoes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return acao == null ? NotFound() : Ok(acao);
        }

        /// <summary>
        /// Cria uma nova ação
        /// </summary>
        /// <param name="model">Enviar Codigo e RazaoSocial</param>
        /// <response code="201">Ação criada com sucesso</response>
        /// <response code="400">Erro ao registrar a ação</response>

        [HttpPost("acoes")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context, 
            [FromBody] CreateAcaoViewModel model)
        {
            if (!ModelState.IsValid) 
                return BadRequest();

            var acao = new Acao
            {
                Date = DateTime.Now,
                Codigo = model.Codigo,
                RazaoSocial = model.RazaoSocial
            };

            try
            {
                await context.Acoes.AddAsync(acao);
                await context.SaveChangesAsync();
                return Created($"v1/acoes/{acao.Id}", acao);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Altera uma ação
        /// </summary>
        /// <param name="model">Enviar Codigo e RazaoSocial para alteração</param>
        /// <param name="id">ID da ação a ser alterada</param>
        /// <response code="200">Informações alteradas com sucesso</response>
        /// <response code="404">ID informado não encontrado</response>
        /// <response code="400">Erro ao registrar a alteração</response>
        [HttpPut("acoes/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context, 
            [FromBody] CreateAcaoViewModel model, 
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var acao = await context.Acoes.FirstOrDefaultAsync(x => x.Id == id);

            if (acao == null) return NotFound();

            try
            {
                acao.RazaoSocial = model.RazaoSocial;
                acao.Codigo = model.Codigo;
                context.Acoes.Update(acao);
                await context.SaveChangesAsync();
                return Ok(acao);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deleta uma ação
        /// </summary>
        /// <param name="id">Informar o ID da ação a ser apagada</param>
        /// <response code="204">Ação deletada com sucesso</response>
        /// <response code="400">Erro ao deletar a alteração</response>

        [HttpDelete("acoes/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            try
            {
                var acao = await context.Acoes.FirstOrDefaultAsync(x => x.Id == id);
                context.Acoes.Remove(acao);
                await context.SaveChangesAsync();
                return NoContent();
            } 
            catch (Exception e) 
            {
                return BadRequest(e.Message);
            }
        }
    }
}
