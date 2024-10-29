using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Bunit;
using NUnit.Framework;

using ContosoCrafts.WebSite.Components;
using ContosoCrafts.WebSite.Services;

namespace UnitTests.Components
{
    //public class CategoryListTests : BunitTestContext
    //{
    //    #region TestSetup

    //    [SetUp]
    //    public void TestInitialize()
    //    {
    //    }

    //    #endregion TestSetup

    //    [Test]
    //    public void ProductList_Valid_Default_Should_Return_Content()
    //    {
    //        // Arrange
    //        Services.AddSingleton<JsonFileCategoryService>(TestHelper.CategoryService);

    //        // Act
    //        var page = RenderComponent<CategoryList>();

    //        // Get the Cards retrned
    //        var result = page.Markup;

    //        // Assert
    //        Assert.That(result.Contains("Object-oriented programming"), Is.EqualTo(true));
    //    }
    //}
}