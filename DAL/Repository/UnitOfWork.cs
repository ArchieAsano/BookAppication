using DAL.Interface;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly BookStoreDBContext _dbContext;
        public UnitOfWork(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(_dbContext);
        }    
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
