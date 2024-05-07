using Dtos.Pagination;
using Microsoft.EntityFrameworkCore;
public class ProductService
{
    private readonly AppDBContext _appDbContext;
    public ProductService(AppDBContext appDBContext)
    {
        _appDbContext = appDBContext;
    }

    public async Task<PaginationResult<Product>> GetAllProductService(int pageNumber, int pageSize)
    {
        var totalCount = _appDbContext.Products.Count();
        var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
        var page = await _appDbContext.Products
            .OrderByDescending(b => b.CreatedAt)
            .ThenByDescending(b => b.ProductID)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(p => p.Category)
            .ToListAsync();

        return new PaginationResult<Product>
        {
            Items = page,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };
    }

    public async Task<Product?> GetProductById(Guid productId)
    {
        return await _appDbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductID == productId);
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
        await _appDbContext.Products.AddAsync(product);
        await _appDbContext.SaveChangesAsync();
        return product.ProductID;
    }

    public async Task<bool> UpdateProductService(Guid productId, ProductModel updateProduct)
    {
        var existingProduct = await _appDbContext.Products.FirstOrDefaultAsync(p => p.ProductID == productId);
        if (existingProduct != null)
        {
            existingProduct.ProductName = updateProduct.ProductName;
            existingProduct.Description = updateProduct.Description;
            existingProduct.Quantity = updateProduct.Quantity;
            existingProduct.Price = updateProduct.Price;
            existingProduct.CategoryId = updateProduct.CategoryID;
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteProductService(Guid productId)
    {
        var productToRemove = await _appDbContext.Products.FirstOrDefaultAsync(p => p.ProductID == productId);
        if (productToRemove != null)
        {
            _appDbContext.Products.Remove(productToRemove);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    //Search service 



    public async Task<IEnumerable<Product>> SearchProductsAsync(string searchKeyword, int page, int pageSize)
    {
        return await _appDbContext.Products
            .Where(p => p.ProductName.Contains(searchKeyword))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}