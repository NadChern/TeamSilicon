using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages
{
    public class IndexModel : PageModel
    {
        //Hi Mike
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger,
            JsonFileProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }

        public JsonFileProductService ProductService { get; }
        public IEnumerable<ProductModel> Products { get; private set; }

        public void OnGet()
        {
            Products = ProductService.GetAllData();
        }
    }
}