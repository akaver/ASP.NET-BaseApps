using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Interfaces.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using NLog;

namespace WebApi.DAL.Repositories
{
    public class WebApiRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IAuthenticationManager _authenticationManager;
        protected HttpClient HttpClient;
        protected string EndPoint;
        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public WebApiRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
            HttpClient = httpClient;
            EndPoint = endPoint;
        }


        public List<T> All
        {
            get
            {
                var response = HttpClient.GetAsync(EndPoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var res = response.Content.ReadAsAsync<List<T>>().Result;
                    return res;
                }
                _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    LogUserOutAndRedirectToLogin();
                }
                return new List<T>();
            }
        }

        public List<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException("Not implemented in Web API!?!?");
        }

        public T GetById(params object[] id)
        {
            var uri = id[0];
            for (int i = 1; i < id.Length; i++)
            {
                uri = uri + "/" + id[i];
            }

            var response = HttpClient.GetAsync(EndPoint + uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<T>().Result;
                return res;
            }
            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                LogUserOutAndRedirectToLogin();
            }
            return null;

        }

        public void Add(T entity)
        {
            var response = HttpClient.PostAsJsonAsync(EndPoint, entity).Result;
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                LogUserOutAndRedirectToLogin();
            }
            else if (!response.IsSuccessStatusCode)
            {
                _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode);
                throw new Exception(response.RequestMessage.RequestUri + " - " + response.StatusCode);
            }
        }

        public void Update(T entity)
        {
            // break restful practices, dont use endpoint/id
            // id is already in entity
            // baseurl http://...../api/
            // enpoint ControllerName/
            var response = HttpClient.PutAsJsonAsync(EndPoint, entity).Result;

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                LogUserOutAndRedirectToLogin();
            }
            else if (!response.IsSuccessStatusCode)
            {
                _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode);
                throw new Exception(response.RequestMessage.RequestUri + " - " + response.StatusCode);
            }
        }

        public void Delete(T entity)
        {
            // baseaddr+EndPoint+Delete+/
            // PUT http://..../api/Persons/Delete/

            var response = HttpClient.PutAsJsonAsync(EndPoint + nameof(Delete) + "/", entity).Result;
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                LogUserOutAndRedirectToLogin();
            }
            else if (!response.IsSuccessStatusCode)
            {
                _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode);
                throw new Exception(response.RequestMessage.RequestUri + " - " + response.StatusCode);
            }
        }

        public void Delete(params object[] id)
        {
            var uri = id[0];
            for (int i = 1; i < id.Length; i++)
            {
                uri = uri + "/" + id[i];
            }
            // DELETE http://..../api/controller/id0/id1/id2/....
            var response = HttpClient.DeleteAsync(EndPoint + uri).Result;
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                LogUserOutAndRedirectToLogin();
            }
            else if (!response.IsSuccessStatusCode)
            {
                _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode);
                throw new Exception(response.RequestMessage.RequestUri + " - " + response.StatusCode);
            }
        }
        public void Dispose()
        {
        }

        private void LogUserOutAndRedirectToLogin()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Redirect(@"~/Account/Login");
        }
    }
}
