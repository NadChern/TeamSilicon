using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        public CategoryController(JsonFileCategoryService categoryService)
        {
            CategoryService = categoryService;
        }

        public JsonFileCategoryService CategoryService { get; }

        [HttpGet]
        public IEnumerable<CategoryModel> Get()
        {
            return CategoryService.GetAllData();
        }
    }
}