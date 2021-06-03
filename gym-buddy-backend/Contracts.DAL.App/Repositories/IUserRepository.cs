using Contracts.DAL.Base.Repositories;
using DAL.App.DTO.Identity;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserRepository : IBaseRepository<AppUser>, IUserRepositoryCustom<AppUser>
    {
    }

    public interface IUserRepositoryCustom<TEntity>
    {
    }
}