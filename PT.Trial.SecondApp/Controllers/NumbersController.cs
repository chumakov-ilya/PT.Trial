using System.Collections.Generic;
using System.Linq;
using PT.Trial.Common;
using System.Web.Http;
using System.Web.Http.Results;

namespace PT.Trial.SecondApp.Controllers
{
    public class NumbersController : ApiController
    {
        public IHttpActionResult Post([FromBody]Number current)
        {
            var next = CalcService.GetNextNumber(current);

            string threadId = ReadThreadId();

            BusService.Publish(next, null);

            return Ok();
        }

        private string ReadThreadId()
        {
            string value = null;

            IEnumerable<string> headerValues;

            if (Request.Headers.TryGetValues("pt-thread-id", out headerValues))
            {
                value = headerValues.FirstOrDefault();
            }

            return value;
        }
    }
}
