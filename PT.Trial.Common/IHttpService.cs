using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PT.Trial.Common
{
    public interface IHttpService
    {
        Task<bool> SendAsync(Number number, string calculationId);

        string ReadCalculationId(HttpRequestHeaders headers);
    }
}