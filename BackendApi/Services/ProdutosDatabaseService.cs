
using BackendApi.Database;

namespace BackendApi.Services
{
    public class ProdutosDatabaseService : IProdutosService
    {
        private ApplicationDbContext banco;

        public ProdutosDatabaseService(ApplicationDbContext banco)
        {
            this.banco = banco;
        }
        public void Adicionar(Produto novoProduto)
        {
            banco.produtos.Add(novoProduto);
            banco.SaveChanges();
        }

        public Produto Atualizar(int id, Produto produtoAtualizado)
        {
            var produto = banco.produtos.FirstOrDefault(x => x.Id == id);

            if(produto is null)
            {
                return null;
            }

            produto.Nome = produtoAtualizado.Nome;
            produto.Preco = produtoAtualizado.Preco;
            produto.Estoque = produtoAtualizado.Estoque;

            banco.SaveChanges();
            return produto;

        }

        public Produto ObterPorId(int id)
        {
            return banco.produtos.FirstOrDefault(x => x.Id == id);
        }

        public List<Produto> ObterTodos()
        {
            return banco.produtos.ToList();
        }

        public bool Remover(int id)
        {
            var produto = banco.produtos.FirstOrDefault(x => x.Id == id);

            if(produto is null)
            {
                return false;
            }

            banco.produtos.Remove(produto);
            banco.SaveChanges();

            return true;
        }
    }
}
