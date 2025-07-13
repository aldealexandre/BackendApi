using BackendApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private IProdutosService produtosService;

        public ProdutosController(IProdutosService produtosService)
        {
            this.produtosService = produtosService;
        }


        [HttpGet]
        public ActionResult<List<Produto>> Get()
        {
            return Ok(produtosService.ObterTodos());
        }

        [HttpGet("{id}")]
        public ActionResult<Produto> GetById(int id)
        {
            var produto = produtosService.ObterPorId(id);
            if(produto is null)
            {
                return NotFound($"Produto com ID {id} não encontrado.");
            }
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult Post(Produto novoProduto)
        {
            produtosService.Adicionar(novoProduto);
            return Created();
        }

        [HttpPut("{id}")]
        public ActionResult<Produto> Put(int id, Produto produtoAtualizado)
        {
            var produto = produtosService.Atualizar(id, produtoAtualizado);
            if(produto is null)
            {
                return NotFound($"Produto com ID {id}não encontrado.");
            }

            return Ok(produto);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var removeu = produtosService.Remover(id);

            if(removeu == false)
            {
                return NotFound($"Produto com ID {id}não encontrado.");
            }
            return NoContent();
        }
    }
}
