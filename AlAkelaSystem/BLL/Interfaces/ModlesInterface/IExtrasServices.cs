using BLL.Helper;
using DTO.ExtrasDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.ModlesInterface
{
    public interface IExtrasServices
    {
        Task<ApiResponse<IEnumerable<ExtrasResponseDto>>> GetAllExtrasAsync();
        Task<ApiResponse<ExtrasResponseDto>> GetExtrasByIdAsync(int id);
        Task<ApiResponse<ExtrasResponseDto>> CreateExtrasAsync(CreateExtrasDto extras);
        Task<ApiResponse<ExtrasResponseDto>> UpdateExtrasAsync(int id, UpdateExtrasDto extras);
        Task<ApiResponse<bool>> DeleteExtrasAsync(int id);
    }
}
