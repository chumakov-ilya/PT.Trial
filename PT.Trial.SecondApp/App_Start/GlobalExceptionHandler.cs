using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace PT.Trial.SecondApp
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new InternalServerErrorResult();
        }

        public class InternalServerErrorResult : IHttpActionResult
        {
            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var message = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                message.Content = new StringContent(HttpStatusCode.InternalServerError.ToString());
                return Task.FromResult(message);
            }
        }
    }
}