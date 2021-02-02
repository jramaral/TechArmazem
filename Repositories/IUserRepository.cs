using System.Threading.Tasks;
using ArmazemAPI.Models;

namespace ArmazemAPI.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task<Usuario> GetUsuario(string username, string password);
    }
}