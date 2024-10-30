using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Represents page, which displays a list of categories
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Logger instance for logging events and diagnostics related to page.
        /// </summary>
        private readonly ILogger<IndexModel> _logger;

        /// <summary>
        /// Initializes a new instance of IndexModel class.
        /// </summary>
        /// <param name="logger">The logger instance used to log events and diagnostics</param>
        /// <param name="categoryService">The service used to retrieve category data</param>
        public IndexModel(ILogger<IndexModel> logger,
            JsonFileCategoryService categoryService)
        {
            _logger = logger;
            CategoryService = categoryService;
        }

        /// <summary>
        /// Gets the service responsible for accessing category data.
        /// </summary>
        public JsonFileCategoryService CategoryService { get; }

        /// <summary>
        /// Gets the collection of categories to be displayed on page.
        /// </summary>
        public IEnumerable<CategoryModel> Categories { get; private set; }


        /// <summary>
        /// Handles HTTP GET requests to retrieve all categories and assigns
        /// them to Categories property.
        /// </summary>
        public void OnGet()
        {
            // Retrieve all categories from the service
            Categories = CategoryService.GetAllData();
        }
    }
}
