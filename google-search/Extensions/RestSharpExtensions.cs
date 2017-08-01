using RestSharp;
using System.Threading.Tasks;

namespace google_search.Extensions
{
    internal static class RestSharpExtensions
    {
        internal static async Task<IRestResponse<T>> ExecuteAsync<T>(this IRestClient client, IRestRequest request)
        {
            var taskCompletionSource = new TaskCompletionSource<IRestResponse<T>>();
            client.ExecuteAsync<T>(request, (response, asyncHandle) =>
            {
                if (response.ResponseStatus == ResponseStatus.Error)
                    taskCompletionSource.SetException(response.ErrorException);
                else
                    taskCompletionSource.SetResult(response);
            });

            return await taskCompletionSource.Task;
        }
    }
}
