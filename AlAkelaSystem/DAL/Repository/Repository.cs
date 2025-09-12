using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class Repository<T> :IGenericRepository<T> where T : class
    {
        private readonly AlAkelaDBcontext _context;
        private readonly DbSet<T> _Dbset;
        public Repository(AlAkelaDBcontext context)
        {
            _context = context;
            _Dbset = _context.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
            var Entity = await _Dbset.AddAsync(entity);

            return Entity.Entity;
        }
        public async Task<IEnumerable<T>> GetAll() => await _Dbset.ToListAsync();


        public async Task<bool> Delete(int id)
        {
            var entiy = await _Dbset.FindAsync(id);
            if (entiy != null)
            {
                _Dbset.Remove(entiy);

                return true;
            }
            return false;
        }



        public async Task<T> GetById(int id) => await _Dbset.FindAsync(id);


        public IQueryable<T> Query()
        {
            return _Dbset.AsQueryable();
        }


        public void Update(T entity)
        {
            _Dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
       
    }
}
