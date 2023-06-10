using Core.Domain.Shared.Contacts;
using Refit;

namespace Infrastructure.Shared.Helper
{
    public class RefitHelper
    {
        private readonly IAuthenticatedUser _authenticatedUser;
        public RefitHelper(IAuthenticatedUser authenticatedUser)
        {
            _authenticatedUser = authenticatedUser;
        }
        public async Task<T> RefitApiCall<T>(string apiBasicUri)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(apiBasicUri);
            httpClient.DefaultRequestHeaders.Add("Processcall", "true");

            return RestService.For<T>(httpClient);
        }

        public async Task<T> RefitApiCallWithoutToken<T>(string apiBasicUri)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(apiBasicUri);
            return RestService.For<T>(httpClient);
        }

    }
}
