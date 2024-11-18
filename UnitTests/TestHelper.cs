using Blazored.LocalStorage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Moq;
using ContosoCrafts.WebSite.Services;

namespace UnitTests
{
    /// <summary>
    /// Test Helper to hold the web start settings including:
    /// HttpClient, Action Context, The View Data and Temp Data,
    /// services
    /// </summary>
    public static class TestHelper
    {
        // Mocked IWebHostEnvironment to simulate a web hosting environment
        public static Mock<IWebHostEnvironment> MockWebHostEnvironment;
        
        // Factory for creating IUrlHelper instances
        public static IUrlHelperFactory UrlHelperFactory;
        
        // Default HTTP context for simulating HTTP requests and responses
        public static DefaultHttpContext HttpContextDefault;
        
        // IWebHostEnvironment instance representing the hosting environment
        public static IWebHostEnvironment WebHostEnvironment;
        
        // Model state dictionary to track validation state
        public static ModelStateDictionary ModelState;
        
        // Action context for MVC actions
        public static ActionContext ActionContext;
        
        // Metadata provider for handling metadata about models
        public static EmptyModelMetadataProvider ModelMetadataProvider;
        
        // View data dictionary to store data for views
        public static ViewDataDictionary ViewData;
        
        // Temp data dictionary to store temporary data between requests
        public static TempDataDictionary TempData;
        
        // Page context for Razor Pages, including routing and HTTP context
        public static PageContext PageContext;
        
        // Service to manage categories, used in unit tests
        public static JsonFileCategoryService CategoryService;
        
        // Service to manage flashcards, used in unit tests
        public static JsonFileFlashcardService FlashcardService;
        
        // Service to manage local storage, used in unit tests
        public static LocalStorageFlashcardService LocalStorageFlashcardService;

        /// <summary>
        /// Default Constructor
        /// </summary>
        static TestHelper()
        {
            // Mock IWebHostEnvironment with predefined paths
            MockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            MockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            MockWebHostEnvironment.Setup(m => m.WebRootPath).Returns(TestFixture.DataWebRootPath);
            MockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns(TestFixture.DataContentRootPath);

            // Default HTTP context initialization
            HttpContextDefault = new DefaultHttpContext()
            {
                TraceIdentifier = "trace",
            };
            HttpContextDefault.HttpContext.TraceIdentifier = "trace";

            // Initialize model state dictionary
            ModelState = new ModelStateDictionary();

            // Initialize action context with HTTP context and routing data
            ActionContext = new ActionContext(HttpContextDefault, HttpContextDefault.GetRouteData(), new PageActionDescriptor(), ModelState);

            // Metadata provider for managing model metadata
            ModelMetadataProvider = new EmptyModelMetadataProvider();
            
            // Initialize view data with the metadata provider and model state
            ViewData = new ViewDataDictionary(ModelMetadataProvider, ModelState);
            
            // Initialize temp data with HTTP context and a mocked ITempDataProvider
            TempData = new TempDataDictionary(HttpContextDefault, Mock.Of<ITempDataProvider>());

            // Initialize page context with action context and view data
            PageContext = new PageContext(ActionContext)
            {
                ViewData = ViewData,
                HttpContext = HttpContextDefault
            };

            // Initialize the JsonFileCategoryService with mocked environment
            CategoryService = new JsonFileCategoryService(MockWebHostEnvironment.Object);

            // Initialize JsonFileFlashcardService with the mocked environment
            FlashcardService = new JsonFileFlashcardService(MockWebHostEnvironment.Object);
            
            // Mock ILocalStorageService for unit testing LocalStorageFlashcardService
            var mockLocalStorageService = new Mock<ILocalStorageService>();

            // Initialize LocalStorageFlashcardService with the mocked ILocalStorageService
            LocalStorageFlashcardService = new LocalStorageFlashcardService(mockLocalStorageService.Object);
        }
    }
}