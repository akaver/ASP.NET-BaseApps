using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Repositories;
using Interfaces.UOW;

namespace WebApi.DAL
{
    public class WebApiUOW : IUOW, IDisposable
    {
        /// <summary>
        /// Not used in Web API
        /// </summary>
        public void Commit()
        {
           
        }

        public T GetRepository<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IContactTypeRepository ContactTypes { get; }
        public IMultiLangStringRepository MultiLangStrings { get; }
        public ITranslationRepository Translations { get; }
        public IPersonRepository Persons { get; }
        public IContactRepository Contacts { get; }
        public IArticleRepository Articles { get; }
        public IUserIntRepository UsersInt { get; }
        public IUserRoleIntRepository UserRolesInt { get; }
        public IRoleIntRepository RolesInt { get; }
        public IUserClaimIntRepository UserClaimsInt { get; }
        public IUserLoginIntRepository UserLoginsInt { get; }

        public void Dispose()
        {
        }

    }
}
