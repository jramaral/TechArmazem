using System.Collections.Generic;
using System.Threading.Tasks;
using ArmazemAPI.Dto.POCO;
using ArmazemAPI.Models;

namespace ArmazemAPI.Repositories
{
    public interface IProdutoRepository : IRepository
    {
        Task<List<Produto>> RetornarTodosProdutosAsync();
        Task<Produto> RetornarProdutoPorIdAsync(int id);

        bool AplicarDesconto(PocoProd prod);


    }
}