using api.Controllers;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("/api/products")]
public class ProductController:ControllerBase{

    private readonly ProductService _productService;
    public ProductController(AppDBContext appDBContext){
        _productService=new ProductService(appDBContext);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProduct([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
    {
        try{
            var product=await _productService.GetAllProductService(pageNumber, pageSize);
            if(product == null){
                return ApiResponse.NotFound("No Product Found");
            }
            return ApiResponse.Success(product, "All products are returned successfully");
            

        }catch(Exception e){
            Console.WriteLine($"An error occurred here when we tried get all the products");
            return ApiResponse.ServerError(e.Message);
        }
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult>GetUser(string productId){
        try{
            if(!Guid.TryParse(productId,out Guid productIdGuid)){
                return ApiResponse.BadRequest("Invalid product ID format");
            }
            var product =await _productService.GetProductById(productIdGuid);
            if(product==null){
                return ApiResponse.NotFound("No Product Found");

            }else{

                return ApiResponse.Success(product, "single product is returned successfully");

            }
        }catch(Exception e){
            Console.WriteLine("$An error occurred here we tried get the category");
            return ApiResponse.ServerError(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductModel newProduct)
    {
        try
        {
            var response =await _productService.AddProductAsync(newProduct);
            return ApiResponse.Created(response);
        }catch(Exception ex){
            return ApiResponse.ServerError(ex.Message);
        }
    }

    [HttpPut("{productId:guid}")]
    public async Task<IActionResult> UpdateProduct(Guid productId, ProductModel updateProduct)
    {
        try{
            await _productService.UpdateProductService(productId,updateProduct);
            return ApiResponse.Updated("Product is Updated successfully");
        }catch(Exception e){
            return ApiResponse.ServerError(e.Message);
        }
    }

    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> DeleteProduct(string productId){
        if(!Guid.TryParse(productId,out Guid productIdGuid)){
            return ApiResponse.BadRequest("Invalid product ID format");
        }
        var result = await _productService.DeleteProductService(productIdGuid);
        if(!result){
            return NotFound();
        }
        return ApiResponse.Deleted("product is Deleted successfully");
    }
}