using Microsoft.AspNetCore.Mvc;
using Microservicos.Models;
using ProductService.Infrastructure.Data;

namespace ProductService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly EstoqueDbContext _context;

        public ProdutoController(EstoqueDbContext context)
        {
            _context = context;
        }

        // POST: api/produto
        [HttpPost]
        public async Task<IActionResult> CriarProduto([FromBody] Produto produto)
        {
            if (produto == null)
                return BadRequest("Produto inválido");

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterPorId), new { id = produto.Id }, produto);
        }

        // GET: api/produto
        [HttpGet]
        public IActionResult ListarProdutos()
        {
            var produtos = _context.Produtos.ToList();
            return Ok(produtos);
        }

        // GET: api/produto/{id}
        [HttpGet("{id}")]
        public IActionResult ObterPorId(Guid id)
        {
            var produto = _context.Produtos.Find(id);
            if (produto == null)
                return NotFound();

            return Ok(produto);
        }
    }
}
