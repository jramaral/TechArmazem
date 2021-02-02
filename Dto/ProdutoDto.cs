using System;
using System.ComponentModel.DataAnnotations;

namespace ArmazemAPI.Dto
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nome do Produto não pode ser nulo.")]
        [StringLength(80, MinimumLength = 4, ErrorMessage = "Nome do produto deve ter entre 4 a 80 caracteres")]
        public string NomeProduto { get; set; }
        public int QtdeEstoque { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal ValorCompra { get; set; }
        public String CategoriaProduto { get; set; }
        
    }

}