using EntityFramework;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
[ApiController]
[Route("/api/products")]
public class ProductController:ControllerBase{

    private readonly ProductService? _productService;
    public ProductController(AppDBContext appDBContext){
        _productService=new ProductService(appDBContext);

    }
    [HttpGet]
    public async Task<IActionResult> GetAllProduct()
    {
        try{
            var product=await _productService.GetAllProductService();
            if(product.ToList().Count<1){

                return NotFound (new {success=false,message="No Product Found"});

            }
            return Ok(new{success =true,message="All products are returned successfully",data=product});


        }catch(Exception e){
            Console.WriteLine($"An error occured here when we tried get all the products");
            return StatusCode(500,e.Message);
        }
    }
    [HttpGet("{productId}")]
    public async Task<ActionResult>GetUser(string productId){
        try{
            if(!Guid.TryParse(productId,out Guid productIdGuid)){
                return BadRequest("Invalid product ID format");
            }
            var product =await _productService.GetProductById(productIdGuid);
            if(product==null){
                return NotFound(new{success=false,message="No product Found"});

            }else{
                return Ok (new {success=true,message="single product is returned successfully",data=product});
            }
        }catch(Exception e){
            Console.WriteLine("$An error occurred here we tried get the category");
            return StatusCode(500,e.Message);
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
            Console.WriteLine("Hey error is here!!");
            return ApiResponse.ServerError(ex.Message);
           
        }
    }
    [HttpPut("{productId:guid}")]
    public async Task<IActionResult> UpdateProduct(Guid productId,ProductModel updateProduct)
    {
        try{
            await _productService.UpdateProductService(productId,updateProduct);
            return Ok ("Product is Updated successfully");
        }catch(Exception e){
            return StatusCode(500,e.Message);
        }

    }
    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> DeleteProduct(string productId){
        if(!Guid.TryParse(productId,out Guid productIdGuid)){
            return BadRequest("Invalid product ID format");
        }
        var result =await _productService.DeleteProductService(productIdGuid);
        if(!result){
            return NotFound();
        }
        return Ok ("product is Deleted successfully");
    }


}