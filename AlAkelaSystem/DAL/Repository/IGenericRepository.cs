using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IGenericRepository <T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        // GetBY Id
        Task<T> GetById(int id);
        //Add 
        Task<T> Add(T entity);
        // Updata 
        void Update(T entity);
        // Delete 
        Task<bool> Delete(int id);
        IQueryable<T> Query();

        Task<int> SaveChangesAsync();
    }
}
