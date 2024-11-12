using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    public class PrivacyModel : PageModel
    {
        /// <summary>
        /// Logger instance for logging information and errors related to the Privacy page.
        /// </summary>
        private readonly ILogger<PrivacyModel> _logger;

        /// <summary>
        /// Initializes a new instance of PrivacyModel class.
        /// </summary>
        /// <param name="logger">The logger instance used to log events and diagnostics.</param>
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// A flag indicating that OnGet was successful.
        /// </summary>
        public bool IsLoaded { get; set; }

        /// <summary>
        /// Handles the HTTP GET request for the Privacy page.
        /// </summary>
        public void OnGet()
        {
            IsLoaded = true;
        }
    }
}
