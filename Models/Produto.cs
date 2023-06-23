using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArmazemAPI.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public int QtdeEstoque { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal ValorCompra { get; set; }
        
        public Categoria CategoriaProduto { get; set; } 
      

    }

    public enum Categoria
    {
        DOCE=0,
        SALGADO=1,
    }
}