using Skinet.API.Entities;
using Skinet.Core.Repository;
using Skinet.Infrastructure.Data.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WarehouseDbContext _context;
        private Hashtable _repository;

        public UnitOfWork(WarehouseDbContext context )
        {
            _context = context;
            _repository = new Hashtable();
        }
        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            var type = typeof(T).Name;
            if (!_repository.ContainsKey(type))
            {
                var repo = new GenericRepository<T>(_context);
                _repository.Add(type, repo);

            }

            return _repository[type] as IGenericRepository<T>;

        }

        public async Task<int> Complete()
        {
            try
            {
                return await _context.SaveChangesAsync();

            }
            catch (Exception ex )
            {

                Console.WriteLine($"an error happend {ex.Message}");
                throw;
            }

        
        
        
        }

        public async ValueTask DisposeAsync()
             => await _context.DisposeAsync();
        

        
    }
    
}
