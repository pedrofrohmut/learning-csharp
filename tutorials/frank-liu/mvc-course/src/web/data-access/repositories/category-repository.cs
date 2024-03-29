using MvcCourse.Web.Models;

namespace MvcCourse.Web.DataAccess.Repositories;

public static class CategoryRepository
{
    private static List<Category> categories = new List<Category>() {
        new Category { CategoryId = 1, Name = "beverage", Description = "beverage" },
        new Category { CategoryId = 2, Name = "bakery", Description = "bakery" },
        new Category { CategoryId = 3, Name = "meat", Description = "meat" },
    };

    public static void AddCategory(Category category)
    {
        var maxId = categories.Max(x => x.CategoryId);
        category.CategoryId = maxId + 1;
        categories.Add(category);
    }

    public static List<Category> FindAllCategories() => categories;

    public static Category? GetCategoryById(int categoryId)
    {
        var category = categories.FirstOrDefault(x => x.CategoryId == categoryId);
        return category != null ? category.Copy() : null;
    }

    public static void UpdateCategory(Category updatedCategory)
    {
        var dbCategory = categories.FirstOrDefault(x => x.CategoryId == updatedCategory.CategoryId);
        if (dbCategory == null) return;
        dbCategory.Name = updatedCategory.Name;
        dbCategory.Description = updatedCategory.Description;
    }

    public static void DeleteCategory(int categoryId)
    {
        var dbCategory = categories.FirstOrDefault(x => x.CategoryId == categoryId);
        if (dbCategory == null) return;
        categories.Remove(dbCategory);
    }
}
