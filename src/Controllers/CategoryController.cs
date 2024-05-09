using api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("/api/categories")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;
    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategory()
    {
        try
        {
            var categories = await _categoryService.GetAllCategoryService();
            if (categories.ToList().Count < 1)
            {
                return ApiResponse.NotFound("No Category Found");
            }
            return ApiResponse.Success(categories, "all categories are returned successfully");

        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred here when we tried get all the category");
            return ApiResponse.ServerError(e.Message);
        }

    }

    [HttpGet("{categoryId:guid}")]
    public async Task<IActionResult> GetCategory(Guid categoryId)
    {
        try
        {
            var category = await _categoryService.GetCategoryById(categoryId);
            if (category == null)
            {
                return ApiResponse.NotFound("No Categories Found");
            }
            else
            {
                return ApiResponse.Success(category, "Category Found");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred here when we tried get the category");
            return ApiResponse.ServerError(e.Message);
        }
    }

    [Authorize(Roles = "Admin")]
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
        catch (DbUpdateException ex) when (ex.InnerException is Npgsql.PostgresException postgresException)
        {
            if (postgresException.SqlState == "23505")
            {
                return ApiResponse.Conflict("Duplicate Name. Category with Name already exists");
            }
            else
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }
        catch (Exception e)
        {

            return ApiResponse.ServerError(e.Message);
        }
    }

    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
    [HttpDelete("{categoryId:guid}")]
    public async Task<IActionResult> DeleteCategory(Guid categoryId)
    {
        try
        {
            var result = await _categoryService.DeleteCategoryService(categoryId);
            if (!result)
            {
                return ApiResponse.NotFound("Category Not Found");
            }
            return ApiResponse.Deleted("Category is deleted successfully");
        }
        catch (Exception e)
        {
            return ApiResponse.ServerError(e.Message);
        }

    }
}
