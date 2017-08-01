using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using google_search.Services;

namespace google_search.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISearchService _searchService;

        public HomeController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public async Task<IActionResult> Index(string search)
        {
            var searchItems = await _searchService.SearchAsync(search, 12);

            return View(searchItems);
        }
    }
}
