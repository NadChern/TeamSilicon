using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Represents Error page
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the current HTTP request.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Indicates whether the RequestId should be displayed.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        /// <summary>
        /// Logger instance for logging error-related events and diagnostics.
        /// </summary>
        private readonly ILogger<ErrorModel> _logger;

        /// <summary>
        /// Initializes a new instance of ErrorModel class.
        /// </summary>
        /// <param name="logger">Logger instance used to log error events and diagnostics.</param>
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            // Assign provided logger to the _logger field.
            _logger = logger;
        }

        /// <summary>
        /// Handles the HTTP GET request for the Error page.
        /// Retrieves the RequestId of the current HTTP request or uses the trace identifier if unavailable.
        /// </summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}