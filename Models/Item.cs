using System.ComponentModel.DataAnnotations.Schema;
using ArmazemAPI.Repositories;

namespace ArmazemAPI.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int  CompraVendaId { get; set; }
        public int Qtde { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public string TipoMovimentacao { get; set; }
        
        public CompraVenda CompraVenda { get; set; }
        public Produto Produto { get; set; }
        
    }
}