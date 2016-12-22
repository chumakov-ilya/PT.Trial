using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using PT.Trial.Common;
using System.Web.Http;
using System.Web.Http.Results;
using Autofac;
using PT.Trial.Common.Contracts;

namespace PT.Trial.SecondApp.Controllers
{
    public class NumbersController : ApiController
    {
        public ICalcService CalcService { get; set; }
        public IBusService BusService { get; set; }
        public IHttpService HttpService { get; set; }

        public NumbersController(ICalcService calcService, IBusService busService, IHttpService httpService)
        {
            CalcService = calcService;
            BusService = busService;
            HttpService = httpService;
        }

        public IHttpActionResult Post([FromBody]Number current)
        {
            var next = CalcService.GetNextNumber(current);

            string calculationId = HttpService.ReadCalculationId(Request.Headers);

            BusService.Send(next, calculationId);

            return Ok();
        }
    }
}
