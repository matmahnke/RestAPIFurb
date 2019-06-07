using DTO;
using System.Threading.Tasks;

namespace BusinessRules.Interfaces
{
    public interface IUsuarioService : IEntityBase<Usuario>
    {
        Task Delete(string email);

        Usuario Filter(string email, string senha);
    }
}
