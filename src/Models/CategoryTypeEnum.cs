namespace ContosoCrafts.WebSite.Models
{
    
    /// <summary>
    /// Enum representing types of categories for flashcards
    /// </summary>
    public enum CategoryTypeEnum
    {
        OOP,
        Python,
        CSharp,
        CPlusPlus,
        Mobile,
        DS
    }

    /// <summary>
    /// Extension methods for CategoryTypeEnum to provide display-friendly names
    /// </summary>
    public static class CategoryTypeEnumExtensions
    {
        
        /// <summary>
        /// Gets the display-friendly name for each category
        /// </summary>
        /// <param name="category">The enum value</param>
        /// <returns>A string representing the display name</returns>
        public static string DisplayName(this CategoryTypeEnum category)
        {
            return category switch
            {
                CategoryTypeEnum.OOP => "OOP",
                CategoryTypeEnum.Python => "Python",
                CategoryTypeEnum.CSharp => "CSharp",
                CategoryTypeEnum.CPlusPlus => "CPlusPlus",
                CategoryTypeEnum.Mobile => "Mobile",
                CategoryTypeEnum.DS => "DS",
                _ => category.ToString()
            };
        }
    }
}