using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Repositories;
using NLog;

namespace WebApi.DAL.Repositories
{
    public class WebApiRepository<T> : IBaseRepository<T> where T : class
    {
        protected HttpClient HttpClient;
        protected string EndPoint;
        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public WebApiRepository(HttpClient httpClient, string endPoint)
        {
            HttpClient = httpClient;
            EndPoint = endPoint;
        }

        public void Dispose()
        {
        }

        public List<T> All {
            get
            {
                var response = HttpClient.GetAsync(EndPoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var res = response.Content.ReadAsAsync<List<T>>().Result;
                    return res;
                }
                _logger.Debug(response.StatusCode);
                return new List<T>();
            }
        }

        public List<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public T GetById(params object[] id)
        {
            var uri = id[0];
            for (int i = 1; i < id.Length; i++)
            {
                uri = uri + "/" + id[i];
            }

            var response = HttpClient.GetAsync(EndPoint+uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<T>().Result;
                return res;
            }

            _logger.Debug(response.StatusCode);
            return null;

        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(params object[] id)
        {
            throw new NotImplementedException();
        }
    }
}
