using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CognitiveLocator.WebAPI.Controllers
{
    [RoutePrefix("api/Find")]
    public class SearchController : ApiController
    {
        [Route("ByFace")]
        public async Task<HttpResponseMessage> ByFace()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [Route("ByName")]
        public async Task<HttpResponseMessage> ByName([FromUri] string name,[FromUri] string lastName)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
