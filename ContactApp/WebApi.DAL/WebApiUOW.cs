using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Repositories;
using Interfaces.UOW;
using Microsoft.Owin.Security;
using NLog.Internal;
using WebApi.DAL.Repositories;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace WebApi.DAL
{
    public class WebApiUOW : IUOW, IDisposable
    {
        private readonly IAuthenticationManager _authenticationManager;

        private readonly IDictionary<Type, Func<HttpClient, IAuthenticationManager, object>> _repositoryFactories;

        private readonly IDictionary<Type, object> _repositories = new Dictionary<Type, object>();

        private readonly HttpClient _httpClient = new HttpClient();

        public WebApiUOW(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
            _repositoryFactories = GetCustomFactories();
            var baseAddr = ConfigurationManager.AppSettings["WebApi_BaseAddress"];
            if (string.IsNullOrWhiteSpace(baseAddr))
            {
                throw new KeyNotFoundException("WebApi_BaseAddress not defined in config!");
            }

            _httpClient.BaseAddress = new Uri(baseAddr);

        }

        private static IDictionary<Type, Func<HttpClient, IAuthenticationManager, object>> GetCustomFactories()
        {
            return new Dictionary<Type, Func<HttpClient, IAuthenticationManager, object>>
            {
                {typeof(IArticleRepository),
                    (httpClient, authenticationManager) =>
                    new ArticleRepository(
                        httpClient,
                        ConfigurationManager.AppSettings["WebApi_EndPoint_Articles"],
                        authenticationManager)
                }
            };
        }

        public T GetRepository<T>() where T : class
        {
            var repo = GetWebApiRepository<T>();
            if (repo == null)
            {
                throw new NotImplementedException($"No repo for type: {typeof(T).FullName}");
            }
            return repo;
        }

        private TRepoType GetWebApiRepository<TRepoType>() where TRepoType : class
        {
            object repo;
            _repositories.TryGetValue(typeof(TRepoType), out repo);
            if (repo != null)
            {
                return (TRepoType)repo;
            }


            return MakeRepository<TRepoType>();
        }

        private TRepoType MakeRepository<TRepoType>() where TRepoType : class
        {
            Func<HttpClient, IAuthenticationManager, object> factory;
            _repositoryFactories.TryGetValue(typeof(TRepoType), out factory);
            if (factory == null)
            {
                throw new NotImplementedException($"No factory for type: {typeof(TRepoType).FullName}");
            }

            // create repo 
            var repo = (TRepoType) factory(_httpClient, _authenticationManager);
      
            //save to dictionary
            _repositories[typeof(TRepoType)] = repo;

            return repo;
        }

        public IContactTypeRepository ContactTypes => GetWebApiRepository<IContactTypeRepository>();
        public IMultiLangStringRepository MultiLangStrings  => GetWebApiRepository<IMultiLangStringRepository>();
        public ITranslationRepository Translations  => GetWebApiRepository<ITranslationRepository>();
        public IPersonRepository Persons  => GetWebApiRepository<IPersonRepository>();
        public IContactRepository Contacts  => GetWebApiRepository<IContactRepository>();
        public IArticleRepository Articles => GetWebApiRepository<IArticleRepository>();
        public IUserIntRepository UsersInt  => GetWebApiRepository<IUserIntRepository>();
        public IUserRoleIntRepository UserRolesInt  => GetWebApiRepository<IUserRoleIntRepository>();
        public IRoleIntRepository RolesInt  => GetWebApiRepository<IRoleIntRepository>();
        public IUserClaimIntRepository UserClaimsInt  => GetWebApiRepository<IUserClaimIntRepository>();
        public IUserLoginIntRepository UserLoginsInt  => GetWebApiRepository<IUserLoginIntRepository>();

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
