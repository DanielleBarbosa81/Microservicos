using Microsoft.AspNetCore.Mvc;
using Microservicos.Models;
using ProductService.Infrastructure.Data;

namespace ProductService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstoqueController : ControllerBase
    {
        private readonly EstoqueDbContext _context;

        public EstoqueController(EstoqueDbContext context)
        {
            _context = context;
        }

        // GET: api/estoque/{produtoId}
        [HttpGet("{produtoId}")]
        public IActionResult ConsultarEstoque(Guid produtoId)
        {
            var item = _context.EstoqueItens.FirstOrDefault(e => e.ProdutoId == produtoId);
            if (item == null)
                return NotFound("Produto não encontrado no estoque");

            return Ok(new { ProdutoId = item.ProdutoId, Quantidade = item.Quantidade });
        }

        // PUT: api/estoque/adicionar/{produtoId}
        [HttpPut("adicionar/{produtoId}")]
        public async Task<IActionResult> AdicionarEstoque(Guid produtoId, [FromQuery] int quantidade)
        {
            var item = _context.EstoqueItens.FirstOrDefault(e => e.ProdutoId == produtoId);
            if (item == null)
                return NotFound("Produto não encontrado no estoque");

            item.Adicionar(quantidade);
            await _context.SaveChangesAsync();

            return Ok(new { ProdutoId = item.ProdutoId, NovaQuantidade = item.Quantidade });
        }

        // PUT: api/estoque/reservar/{produtoId}
        [HttpPut("reservar/{produtoId}")]
        public async Task<IActionResult> ReservarEstoque(Guid produtoId, [FromQuery] int quantidade)
        {
            var item = _context.EstoqueItens.FirstOrDefault(e => e.ProdutoId == produtoId);
            if (item == null)
                return NotFound("Produto não encontrado no estoque");

            var sucesso = item.Reservar(quantidade);
            if (!sucesso)
                return BadRequest("Estoque insuficiente para reserva");

            await _context.SaveChangesAsync();
            return Ok(new { ProdutoId = item.ProdutoId, NovaQuantidade = item.Quantidade });
        }

        // PUT: api/estoque/liberar/{produtoId}
        [HttpPut("liberar/{produtoId}")]
        public async Task<IActionResult> LiberarEstoque(Guid produtoId, [FromQuery] int quantidade)
        {
            var item = _context.EstoqueItens.FirstOrDefault(e => e.ProdutoId == produtoId);
            if (item == null)
                return NotFound("Produto não encontrado no estoque");

            item.Liberar(quantidade);
            await _context.SaveChangesAsync();

            return Ok(new { ProdutoId = item.ProdutoId, NovaQuantidade = item.Quantidade });
        }
    }
}
