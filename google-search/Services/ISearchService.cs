using google_search.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace google_search.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<SearchItemViewModel>> SearchAsync(string searchText, int numberOfItems = 10);
    }
}
