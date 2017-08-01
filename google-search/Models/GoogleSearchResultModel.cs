using google_search.ViewModels;
using System.Collections.Generic;

namespace google_search.Models
{
    public class GoogleSearchResultModel
    {
        public IEnumerable<SearchItemViewModel> Items { get; set; }
    }
}
