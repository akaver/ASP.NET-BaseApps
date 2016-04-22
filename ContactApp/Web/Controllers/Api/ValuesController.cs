using System;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace Web.Controllers.Api
{
    [RoutePrefix("api/Values")]
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public string Get()
        {
            var userName = User.Identity.GetUserName();
            return $"Hello, {userName}."+DateTime.Now;
        }
    }
}