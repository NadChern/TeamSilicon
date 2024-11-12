using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Controllers
{
    /// <summary>
    /// Controller for managing categories
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the CategoryController class
        /// </summary>
        /// <param name="categoryService"></param>
        public CategoryController(JsonFileCategoryService categoryService)
        {
            CategoryService = categoryService;
        }

        /// <summary>
        /// Gets service that provides access to category data
        /// </summary>
        public JsonFileCategoryService CategoryService { get; }


        /// <summary>
        /// Handles HTTP GET requests to retrieve all categories
        /// </summary>
        /// <returns> Collection of CategoryModel objects.</returns>
        [HttpGet]
        public IEnumerable<CategoryModel> Get()
        {
            return CategoryService.GetAllData();
        }
    }
}