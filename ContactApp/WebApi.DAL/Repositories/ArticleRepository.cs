using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Interfaces.Repositories;
using Microsoft.Owin.Security;

namespace WebApi.DAL.Repositories
{
    public class ArticleRepository : WebApiRepository<Article>, IArticleRepository
    {
        public ArticleRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

        public Article FindArticleByName(string articleName)
        {
            //var res =
            //    DbSet.Include(a => a.ArticleHeadline.Translations)
            //        .Include(a => a.ArticleBody.Translations)
            //        .FirstOrDefault(a => a.ArticleName == articleName) ?? new Article()
            //        {
            //            ArticleName = "NotFound",
            //            ArticleHeadline = new MultiLangString("Article not found!"),
            //            ArticleBody = new MultiLangString("Article not found!")
            //        };

            return new Article()
            {
                ArticleName = "NotFound",
                ArticleHeadline = new MultiLangString("Article not found!"),
                ArticleBody = new MultiLangString("Article not found!")
            };
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
