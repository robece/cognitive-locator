using CognitiveLocator.WebAPI.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CognitiveLocator.WebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/Test")]
    public class TestingController : ApiController
    {
        [Route("")]
        public async Task<IHttpActionResult> TestAPI([FromUri] string name, [FromUri] string lastName, [FromUri] string faceId, [FromUri] string idPerson)
        {
            var a = await new SPQuery().SelectPersonByName(name);
            var b = await new SPQuery().SelectPersonByLastName(lastName);
            var c = await new SPQuery().SelectPersonByFaceId(faceId);
            //var d = await new SPQuery().DisablePerson(idPerson);
            var d = await new SPQuery().EnablePerson(idPerson);
            var e = await new SPQuery().UpdateFoundPerson(idPerson, 1, "loc 2");
            return Ok();
        }
    }
}