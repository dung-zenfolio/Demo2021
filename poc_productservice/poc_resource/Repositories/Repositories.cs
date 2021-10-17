using Microsoft.EntityFrameworkCore;
using poc_productdatabase.Entities;
using poc_resource.Repositories.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace poc_resource.Repositories
{
    public class Repository<TEntity, TDbContext> : IRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : BaseEntity
    {
        private readonly DbContext context;
        public Repository(TDbContext _context)
        {
            context = _context;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public void Add(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Added;

        }

        public void Delete(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<TEntity> GetById(string id)
        {
            return await context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
