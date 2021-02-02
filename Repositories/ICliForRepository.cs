using System.Collections.Generic;
using System.Threading.Tasks;
using ArmazemAPI.Models;

namespace ArmazemAPI.Repositories
{
    public interface ICliForRepository: IRepository
    {
        Task<List<ClienteFornecedor>> ClienteTodos();
        Task<List<ClienteFornecedor>> FornecedorTodos();
    }
}