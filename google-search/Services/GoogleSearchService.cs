using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using google_search.ViewModels;
using Microsoft.Extensions.Options;
using google_search.Configs;
using RestSharp;
using google_search.Models;
using google_search.Extensions;

namespace google_search.Services
{
    public class GoogleSearchService : ISearchService
    {
        private readonly GoogleApiConfig _googleConfig;

        public GoogleSearchService(IOptions<GoogleApiConfig> googleConfig)
        {
            _googleConfig = googleConfig.Value;
        }

        public async Task<IEnumerable<SearchItemViewModel>> SearchAsync(string searchText, int numberOfItems)
        {
            if (string.IsNullOrWhiteSpace(searchText) || numberOfItems < 1)
                return Enumerable.Empty<SearchItemViewModel>();

            return await GetSearchResultsAsync(searchText, numberOfItems);
        }

        private async Task<IEnumerable<SearchItemViewModel>> GetSearchResultsAsync(string searchText, int numberOfItems)
        {
            var items = new List<SearchItemViewModel>();
            var client = CreateClient();

            for(int downloaded = 0; downloaded < numberOfItems; downloaded += _googleConfig.MaxResultsPerRequest)
            {
                var itemsNumberToDownload = numberOfItems - downloaded > _googleConfig.MaxResultsPerRequest ? _googleConfig.MaxResultsPerRequest : numberOfItems - downloaded;
                var request = CreateRequest(searchText, itemsNumberToDownload, downloaded + 1);

                var result = await client.ExecuteAsync<GoogleSearchResultModel>(request);

                if (result.Data.Items == null)
                    return items;
                items.AddRange(result.Data.Items);
            }

            return items;
        }

        private IRestClient CreateClient()
            => new RestClient(_googleConfig.URL);
        
        private RestRequest CreateRequest(string searchText, int numberOfItems, int startIndex = 1)
        {
            var request = new RestRequest(Method.GET);
            request.AddQueryParameter(nameof(GoogleApiConfig.Key).ToLower(), _googleConfig.Key);
            request.AddQueryParameter(nameof(GoogleApiConfig.CX).ToLower(), _googleConfig.CX);
            request.AddQueryParameter(nameof(GoogleApiConfig.Fields).ToLower(), _googleConfig.Fields);
            request.AddQueryParameter(_googleConfig.SearchParameterName, searchText);
            request.AddQueryParameter(_googleConfig.StartParameterName, startIndex.ToString());
            request.AddQueryParameter(_googleConfig.NumberParameterName, numberOfItems.ToString());
            return request;
        }
    }
}
