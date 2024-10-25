using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages
{
    
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger,
            JsonFileCategoryService categoryService)
        {
            _logger = logger;
            CategoryService = categoryService;
        }

        public JsonFileCategoryService CategoryService { get; }
        public IEnumerable<CategoryModel> Products { get; private set; }

        public void OnGet()
        {
            Products = CategoryService.GetAllData();
        }
    }
}