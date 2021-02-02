using System;
using System.ComponentModel.DataAnnotations;

namespace ArmazemAPI.Dto
{
    public class CompraVendaDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Cliente Fornecedor não pode ser nulo.")]
        public int ClienteFornecedorId { get; set; }
        public DateTime DataMovimentacao { get; set; } 
        [Required(ErrorMessage = "O tipo de movimentação não pode ser nulo, digite E para [entrada] ou S [saida].")]
        public string TipoMovimentacao { get; set; } //E entrada, S saida
    }
}