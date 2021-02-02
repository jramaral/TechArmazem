using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmazemAPI.Data;
using ArmazemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmazemAPI.Repositories
{
    public class ClienteRepository : ICliForRepository
    {
        public readonly ApiContext _Context;

        public ClienteRepository(ApiContext context)
        {
            _Context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _Context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _Context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _Context.Remove(entity);
        }
        public async Task<bool> SaveChangeAsync()
        {
            return await _Context.SaveChangesAsync() > 0;
        }

        public async Task<List<ClienteFornecedor>> ClienteTodos()
        {
            return await _Context.ClienteFornecedores.Where(a => a.Tipo == TipoCadastro.C).ToListAsync();
        }

        public async Task<List<ClienteFornecedor>> FornecedorTodos()
        {
            return await _Context.ClienteFornecedores.Where(a => a.Tipo == TipoCadastro.F).ToListAsync();
        }
    }
}