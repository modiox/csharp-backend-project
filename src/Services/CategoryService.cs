

public class CategoryService
{
    private AppDBContext _appDbContext;
    public CategoryService(AppDBContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<CategoryModel>> GetAllCategoryService()
    {
        List<CategoryModel> categories = new List<CategoryModel>();
        var dataList = _appDbContext.Categories.ToList();
        dataList.ForEach(row => categories.Add(new CategoryModel
        {
            CategoryID = row.CategoryID,
            Name = row.Name,
            Description = row.Description
        }));
        await Task.CompletedTask;
        return categories.AsEnumerable();
    }

    public Task<Category?> GetCategoryById(Guid categoryId)
    {

        var dataList = _appDbContext.Categories.ToList();
        var category = dataList.Find(category => category.CategoryID == categoryId);
        return Task.FromResult(category);
    }

    //   public static string GenerateSlug(string name){
    //   return  name.ToLower().Replace(" ","-");
    //   }
    public async Task CreateCategoryService(CategoryModel newCategory)
    {
        await Task.CompletedTask;
        Category category = new Category
        {
            CategoryID = Guid.NewGuid(),
            Name = newCategory.Name,
            Description = newCategory.Description
            // newCategory.Slug = GenerateSlug(newCategory.Name);
            // newCategory.CreatedAt = DateTime.Now;
        };
        _appDbContext.Categories.Add(category);// store this user in our database
        _appDbContext.SaveChanges();

    }

    public async Task UpdateCategoryService(Guid categoryId, CategoryModel updateCategory)
    {
        await Task.CompletedTask;
        var existingCategory = _appDbContext.Categories.FirstOrDefault(c => c.CategoryID == categoryId);
        if (existingCategory != null)
        {
            existingCategory.Name = updateCategory.Name;
            existingCategory.Description = updateCategory.Description;
            _appDbContext.SaveChanges();
        }

    }
    public async Task<bool> DeleteCategoryService(Guid categoryId)
    {
        await Task.CompletedTask;
        var categoryToRemove = _appDbContext.Categories.FirstOrDefault(c => c.CategoryID == categoryId);
        if (categoryToRemove != null)
        {
            _appDbContext.Categories.Remove(categoryToRemove);// store this user in our database
            _appDbContext.SaveChanges();
            return true;
        }
        return false;
    }


}