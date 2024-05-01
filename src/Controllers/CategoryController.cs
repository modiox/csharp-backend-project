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
                return NotFound(new { success = false, message = "No Category Found" });
            }
            return Ok(new { success = true, message = "all categories are returned successfully", data = categories });

        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred here when we tried get all the category");
            return StatusCode(500, e.Message);
        }

    }

    [HttpGet("{categoryId:guid}")]
    public async Task<IActionResult> GetCategory(string categoryId)
    {
        try
        {
            if (!Guid.TryParse(categoryId, out Guid categoryIdGuid))
            {
                return BadRequest("Invalid category ID Format");
            }
            var category = await _categoryService.GetCategoryById(categoryIdGuid);
            if (category == null)
            {
                return NotFound(new { success = false, message = "No Category Found" });
            }
            else
            {
                return Ok(new { success = true, message = "single category is returned successfully", data = category });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred here when we tried get the category");
            return StatusCode(500, e.Message);
        }

    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CategoryModel newCategory)
    {
        try
        {
            await _categoryService.CreateCategoryService(newCategory);
            return Ok("Category is Created successfully");
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }

    }

    [HttpPut("{categoryId:guid}")]
    public async Task<IActionResult> UpdateCategory(Guid categoryId, CategoryModel updateCategory)
    {
        try
        {
            await _categoryService.UpdateCategoryService(categoryId, updateCategory);
            return Ok("Category is Updated successfully");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }

    }

    [HttpDelete("{categoryId:guid}")]
    public async Task<IActionResult> DeleteCategory(string categoryId)
    {
        if (!Guid.TryParse(categoryId, out Guid categoryIdGuid))
        {
            return BadRequest("Invalid category ID Format");
        }
        var result = await _categoryService.DeleteCategoryService(categoryIdGuid);
        if (!result)
        {
            return NotFound();
        }
        return Ok("Category is Deleted successfully");
    }


}
