using System.Linq;
using System.Threading.Tasks;
using ArmazemAPI.Data;
using ArmazemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmazemAPI.Repositories
{
    public class CompraVendaRepository : ICompraVendaRepository
    {

        
        public readonly ApiContext _Context;
        public CompraVendaRepository(ApiContext context)
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

        public bool TemEstoque(int prod, int qtd)
        {
            var produto = _Context.Produtos.FirstOrDefault(p => p.Id == prod);
         
            if (produto != null && produto.QtdeEstoque >= qtd)
            {
                return true;
            }

            return false;
        }

  
        public void AtualizaEstoque(int prod)
        {
            var ent = _Context.Items.Where(i => i.TipoMovimentacao == "E" && i.ProdutoId==prod).Sum(s => s.Qtde);
            var outupt = _Context.Items.Where(i => i.TipoMovimentacao == "S" && i.ProdutoId==prod).Sum(s => s.Qtde) * -1;
            var produto = _Context.Produtos.FirstOrDefault(p => p.Id == prod);
            if (produto != null)
            {
                produto.QtdeEstoque = ent + outupt;

                Update(produto);
            }

            _Context.SaveChanges();
        }

        public bool IsVenda(int cod)
        {
            var venda = _Context.CompraVendas.FirstOrDefault(i =>
                i.Id == cod && i.TipoMovimentacao == TipoMovimentacao.S);
            if (venda != null)
            {
                return true;
            }

            return false;
        }
        public bool IsCompra(int cod)
        {
            var venda = _Context.CompraVendas.FirstOrDefault(i =>
                i.Id == cod && i.TipoMovimentacao == TipoMovimentacao.E);
            if (venda != null)
            {
                return true;
            }

            return false;
        }

        

       
    }
}