namespace ArmazemAPI.Repositories
{
    public interface ICompraVendaRepository : IRepository
    {
        bool TemEstoque(int prod, int qtd);
    
        void AtualizaEstoque(int prod);


        bool IsVenda(int cod);
        bool IsCompra(int cod);
    }
}