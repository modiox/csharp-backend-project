using Microsoft.EntityFrameworkCore;
public class ProductService
{

    private readonly AppDBContext _appDbcontext;
    public ProductService(AppDBContext appDBContext)
    {
        _appDbcontext = appDBContext;


    }
    public async Task<IEnumerable<Product>> GetAllProductService()
    {
        return await _appDbcontext.Products.Include(p => p.Category).ToListAsync();

    }
    public async Task<Product?> GetProductById(Guid productId)
    {
        return await _appDbcontext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductID == productId);

    }
    public async Task<Guid> AddProductAsync(ProductModel newProduct)
    {
        Product product = new Product
        {

            ProductID = Guid.NewGuid(),
            ProductName = newProduct.ProductName,
            Description = newProduct.Description,
            Quantity = newProduct.Quantity,
            Price = newProduct.Price,
            CategoryId = newProduct.CategoryID,
            CreatedAt = DateTime.UtcNow

        };
        await _appDbcontext.Products.AddAsync(product);
        await _appDbcontext.SaveChangesAsync();
        return product.ProductID;
    }
    public async Task<bool> UpdateProductService(Guid productId, ProductModel updateproduct)
    {
        var existingProduct = await _appDbcontext.Products.FirstOrDefaultAsync(p => p.ProductID == productId);
        if (existingProduct != null)
        {
            existingProduct.ProductName = updateproduct.ProductName;
            existingProduct.Description = updateproduct.Description;
            existingProduct.Quantity = updateproduct.Quantity;
            existingProduct.Price = updateproduct.Price;
            existingProduct.CategoryId = updateproduct.CategoryID;
            await _appDbcontext.SaveChangesAsync();
            return true;

        }
        return false;
    }
    public async Task<bool> DeleteProductService(Guid productId){
        var productToRemove=await _appDbcontext.Products.FirstOrDefaultAsync(p=>p.ProductID==productId);
        if(productToRemove !=null){
            _appDbcontext.Products.Remove(productToRemove);
            await _appDbcontext.SaveChangesAsync();
            return true;
        }
        return false;
    }
}