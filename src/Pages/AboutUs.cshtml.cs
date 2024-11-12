using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Represents About Us page
    /// </summary>
    public class AboutUsModel : PageModel
    {
        /// <summary>
        /// Logger instance for logging information and errors related to the About Us page.
        /// </summary>
        private readonly ILogger<AboutUsModel> _logger;

        /// <summary>
        /// Initializes a new instance of AboutUsModel class.
        /// </summary>
        /// <param name="logger">The logger instance used to log events and diagnostics.</param>
        public AboutUsModel(ILogger<AboutUsModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// A flag indicates that OnGet was successful
        /// </summary>
        public bool IsLoaded { get; set; }

        /// <summary>
        /// Handles the HTTP GET request for the About Us page.
        /// </summary>
        public void OnGet()
        {
            IsLoaded = true;
        }
    }
}