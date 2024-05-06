using api.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/categories")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;
    public CategoryController(AppDBContext appDbContext)
    {
        _categoryService = new CategoryService(appDbContext);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategory()
    {
        try
        {
            var categories = await _categoryService.GetAllCategoryService();
            if (categories.ToList().Count < 1)
            {
                // return NotFound(new { success = false, message = "No Category Found" });
                return ApiResponse.NotFound("No Category Found");
            }
            // return Ok(new { success = true, message = "all categories are returned successfully", data = categories });
            return ApiResponse.Success(categories, "all categories are returned successfully");

        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred here when we tried get all the category");
            // return StatusCode(500, e.Message);
            return ApiResponse.ServerError(e.Message);
        }

    }

    [HttpGet("{categoryId:guid}")]
    public async Task<IActionResult> GetCategory(string categoryId)
    {
        try
        {
            if (!Guid.TryParse(categoryId, out Guid categoryIdGuid))
            {
                return ApiResponse.BadRequest("Invalid Category ID Format");
            }
            var category = await _categoryService.GetCategoryById(categoryIdGuid);
            if (category == null)
            {
                // return NotFound(new { success = false, message = "No Category Found" });
                return ApiResponse.NotFound("No Categories Found");
            }
            else
            {
                // return Ok(new { success = true, message = "single category is returned successfully", data = category });
                return ApiResponse.Success(category, "Category Found");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred here when we tried get the category");
            return ApiResponse.ServerError(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CategoryModel newCategory)
    {
        try
        {
            var result = await _categoryService.CreateCategoryService(newCategory);
            if (!result)
            {
                return ApiResponse.BadRequest("Something Went Wrong");
            }
            return ApiResponse.Created("Category is created successfully");
        }
        catch (Exception e)
        {

            return ApiResponse.ServerError(e.Message);
        }
    }

    [HttpPut("{categoryId:guid}")]
    public async Task<IActionResult> UpdateCategory(Guid categoryId, CategoryModel updateCategory)
    {
        try
        {
            await _categoryService.UpdateCategoryService(categoryId, updateCategory);
            return ApiResponse.Updated("Category is updated successfully");
        }
        catch (Exception e)
        {
            return ApiResponse.ServerError(e.Message);
        }

    }

    [HttpDelete("{categoryId:guid}")]
    public async Task<IActionResult> DeleteCategory(string categoryId)
    {
        if (!Guid.TryParse(categoryId, out Guid categoryIdGuid))
        {
            return ApiResponse.BadRequest("Invalid Category ID Format");
        }
        var result = await _categoryService.DeleteCategoryService(categoryIdGuid);
        if (!result)
        {
            return ApiResponse.NotFound("Category Not Found");
        }
        return ApiResponse.Deleted("Category is deleted successfully");
    }


}
