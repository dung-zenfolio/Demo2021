using poc_database.Context;
using poc_database.Entities;
using poc_resource.Repositories.IRepository;

namespace poc_resource.Repositories
{
    public class RolesRepository: Repository<Roles, UserDBContext>, IRolesRepository
    {
        public RolesRepository(UserDBContext context): base(context)
        { }
    }
}
