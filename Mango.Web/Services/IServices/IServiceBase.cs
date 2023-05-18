using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IServiceBase : IDisposable
    {
        public ResponseDto Response { get; }
        Task<T> SendAsync<T>(ApiRequest request);
    }
}
