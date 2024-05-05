

using Microsoft.EntityFrameworkCore;

public class CategoryService
{
    private AppDBContext _appDbContext;
    public CategoryService(AppDBContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<Category>> GetAllCategoryService()
    {
        return await _appDbContext.Categories.Include(c => c.Products).ToListAsync();
    }

    public async Task<Category?> GetCategoryById(Guid categoryId)
    {
       return await _appDbContext.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.CategoryID == categoryId);
    }

    //   public static string GenerateSlug(string name){
    //   return  name.ToLower().Replace(" ","-");
    //   }
    public async Task<bool> CreateCategoryService(CategoryModel newCategory)
    {
        Category category = new Category
        {
            CategoryID = Guid.NewGuid(),
            Name = newCategory.Name,
            Description = newCategory.Description
        };
        await _appDbContext.Categories.AddAsync(category);// store this user in our database
        await _appDbContext.SaveChangesAsync();
        return true;

    }

    public async Task<bool> UpdateCategoryService(Guid categoryId, CategoryModel updateCategory)
    {
          var existingCategory = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryID == categoryId);
        if (existingCategory != null)
        {
            existingCategory.Name = updateCategory.Name;
            existingCategory.Description = updateCategory.Description;
            _appDbContext.SaveChanges();
            return true;
        }
        return false;

    }
    public async Task<bool> DeleteCategoryService(Guid categoryId)
    {
        var categoryToRemove = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryID == categoryId);
        if (categoryToRemove != null)
        {
            _appDbContext.Categories.Remove(categoryToRemove);// store this user in our database
            _appDbContext.SaveChanges();
            return true;
        }
        return false;
    }


}