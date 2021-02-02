using System.ComponentModel.DataAnnotations;

namespace ArmazemAPI.Dto
{
    public class ItemDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O Codigo do produto não pode ser vazio/nulo ou zero")]
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "O Codigo da Venda/Compra não pode ser vazio/nulo ou zero")]
        public int  CompraVendaId { get; set; }
        [Required(ErrorMessage = "A quantidade do produto não pode ser vazio/nulo ou zero")]
        public int Qtde { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        [Required(ErrorMessage = "O tipo de movimentação não pode ser vazio/nulo, digite E para [entrada] ou S [saida].")]
        public string TipoMovimentacao { get; set; }
    }
}