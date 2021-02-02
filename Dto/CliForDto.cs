using System.ComponentModel.DataAnnotations;

namespace ArmazemAPI.Dto
{
    public class CliForDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome do Cliente Fornecedor não pode ser nulo.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O tipo não pode ser nulo, escolha C para cliente ou F para fornecedor")]
        public string Tipo { get; set; } 
    }
}