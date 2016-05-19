using System.Collections.Generic;
using Domain.Identity;

namespace Interfaces.Repositories
{
    public interface IUserClaimIntRepository : IUserClaimRepository<UserClaimInt>
    {
    }

    public interface IUserClaimRepository : IUserClaimRepository<UserClaim>
    {
    }

    public interface IUserClaimRepository<TUserClaim> : IBaseRepository<TUserClaim>
        where TUserClaim : class
    {
        List<TUserClaim> AllIncludeUser();
    }
}