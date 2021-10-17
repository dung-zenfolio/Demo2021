using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poc_resource.Repositories.IRepository
{
    public interface IRepository<TEntity>
    {
        Task<List<TEntity>> GetAll();
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        Task<TEntity> GetById(string id);
        Task<int> SaveChangesAsync();
    }
}
