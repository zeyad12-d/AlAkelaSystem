using BLL.Interfaces.ModlesInterface;
using DAL.Models;
using DAL.unitofwork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ExtrasService : IExtrasServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExtrasService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Extras>> GetAllExtrasAsync()
        {
            return await _unitOfWork.ExtrasRepo.GetAll();
        }

        public async Task<Extras> GetExtrasByIdAsync(int id)
        {
            return await _unitOfWork.ExtrasRepo.GetById(id);
        }

        public async Task<Extras> CreateExtrasAsync(Extras extras)
        {
            var createdExtras = await _unitOfWork.ExtrasRepo.Add(extras);
            await _unitOfWork.SaveChangesAsync();
            return createdExtras;
        }

        public async Task<Extras> UpdateExtrasAsync(Extras extras)
        {
            _unitOfWork.ExtrasRepo.Update(extras);
            await _unitOfWork.SaveChangesAsync();
            return extras;
        }

        public async Task<bool> DeleteExtrasAsync(int id)
        {
            var result = await _unitOfWork.ExtrasRepo.Delete(id);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }
    }
}
