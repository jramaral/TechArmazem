using System;

namespace ArmazemAPI.Models
{
    public class CompraVenda
    {
        public int Id { get; set; }
        public int ClienteFornecedorId { get; set; }
        public DateTime DataMovimentacao { get; set; } = DateTime.Now;
        public TipoMovimentacao TipoMovimentacao { get; set; } //E entrada, S saida
        
        public ClienteFornecedor ClienteFornecedor { get; set; }
        
    }

    public enum TipoMovimentacao
    {
        E,
        S
    }
}