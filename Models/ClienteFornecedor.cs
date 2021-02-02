namespace ArmazemAPI.Models
{
    public class ClienteFornecedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public TipoCadastro Tipo { get; set; } //C cliente, F fornecedor
        
    }

    public enum TipoCadastro
    {
        C,
        F,
    }
}