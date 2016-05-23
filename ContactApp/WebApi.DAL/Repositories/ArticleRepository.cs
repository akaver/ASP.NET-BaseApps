using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Interfaces.Repositories;

namespace WebApi.DAL.Repositories
{
    public class ArticleRepository : WebApiRepository<Article>, IArticleRepository
    {
        public ArticleRepository(HttpClient httpClient, string endPoint) : base(httpClient, endPoint)
        {
        }

        public Article FindArticleByName(string articleName)
        {
            throw new NotImplementedException();
        }

        public List<Article> AllWithTranslations()
        {
            throw new NotImplementedException();
        }

        public void DeleteArticleWithTranslations(params object[] id)
        {
            throw new NotImplementedException();
        }
    }
}
