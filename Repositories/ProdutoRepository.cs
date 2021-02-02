using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmazemAPI.Data;
using ArmazemAPI.Dto.POCO;
using ArmazemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmazemAPI.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        public readonly ApiContext _Context;
        public ProdutoRepository(ApiContext context)
        {
            _Context = context;
            _Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
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
    

        public async Task<List<Produto>> RetornarTodosProdutosAsync()
        {
            var produtos = await _Context.Produtos.ToListAsync();
            return produtos;
        }

        public async Task<Produto> RetornarProdutoPorIdAsync(int id)
        {
            var produto = await _Context.Produtos.Where(c => c.Id == id).FirstOrDefaultAsync();
            return produto;
        }

        public bool AplicarDesconto(PocoProd prod)
        {
            try
            {
                var produto =  _Context.Produtos.FirstOrDefault(c => c.Id == prod.ProdutoId);
                if (produto != null)
                    produto.ValorVenda -= (produto.ValorVenda * Convert.ToDecimal(prod.Desconto)) / 100;
                
                
                Update(produto);
                _Context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
    }
}