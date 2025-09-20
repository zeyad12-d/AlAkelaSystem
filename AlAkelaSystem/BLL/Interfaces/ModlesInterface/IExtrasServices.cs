using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.ModlesInterface
{
    public interface IExtrasServices
    {
        Task<IEnumerable<Extras>> GetAllExtrasAsync();
        Task<Extras> GetExtrasByIdAsync(int id);
        Task<Extras> CreateExtrasAsync(Extras extras);
        Task<Extras> UpdateExtrasAsync(Extras extras);
        Task<bool> DeleteExtrasAsync(int id);
    }
}
