using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Repositories;
using Interfaces.UOW;
using NLog.Internal;
using WebApi.DAL.Repositories;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace WebApi.DAL
{
    public class WebApiUOW : IUOW, IDisposable
    {

        private readonly IDictionary<Type, Func<HttpClient, string, object>> _repositoryFactories;

        private IDictionary<Type, object> _repositories;

        private readonly HttpClient _httpClient = new HttpClient();

        public WebApiUOW()
        {
            _repositoryFactories = GetCustomFactories();
            _repositories = new Dictionary<Type, object>();
            var baseAddr = ConfigurationManager.AppSettings["WebApi_BaseAddress"];
            if (string.IsNullOrWhiteSpace(baseAddr))
            {
                throw new KeyNotFoundException("WebApi_BaseAddress not defined in config!");
            }

            _httpClient.BaseAddress = new Uri(baseAddr);

        }

        private static IDictionary<Type, Func<HttpClient, string, object>>  GetCustomFactories()
        {
            return new Dictionary<Type, Func<HttpClient, string, object>>
            {
                {typeof(IArticleRepository),  (httpClient, endPoint) => new ArticleRepository(httpClient, endPoint)}
            };
        }

        public T GetRepository<T>() where T : class
        {
            return null;
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

        /// <summary>
        /// Not used in Web API
        /// </summary>
        public void Commit()
        {

        }

        public void Dispose()
        {
        }

    }
}
