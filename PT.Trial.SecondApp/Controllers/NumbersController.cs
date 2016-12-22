using System.Collections.Generic;
using System.Linq;
using PT.Trial.Common;
using System.Web.Http;
using System.Web.Http.Results;
using Autofac;

namespace PT.Trial.SecondApp.Controllers
{
    public class NumbersController : ApiController
    {
        public IHttpActionResult Post([FromBody]Number current)
        {
            var CalcService = Root.CreateContainer().Resolve<ICalcService>();
            var BusService = Root.CreateContainer().Resolve<IBusService>();

            var next = CalcService.GetNextNumber(current);

            string threadId = ReadCalculationId();

            BusService.Publish(next, threadId);

            return Ok();
        }

        private string ReadCalculationId()
        {
            string value = null;

            IEnumerable<string> headerValues;

            if (Request.Headers.TryGetValues("pt-calculation-id", out headerValues))
            {
                value = headerValues.FirstOrDefault();
            }

            return value;
        }
    }
}
