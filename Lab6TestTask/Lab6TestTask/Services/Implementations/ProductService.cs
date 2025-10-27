using Lab6TestTask.Data;
using Lab6TestTask.Enums;
using Lab6TestTask.Models;
using Lab6TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab6TestTask.Services.Implementations;

/// <summary>
/// ProductService.
/// Implement methods here.
/// </summary>
public class ProductService : IProductService
{
    private readonly ApplicationDbContext _dbContext;

    public ProductService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product> GetProductAsync()
    {
        var product = await _dbContext.Products
                .Where(p => p.Status == ProductStatus.Reserved)
                .OrderByDescending(p => p.Price)
                .FirstOrDefaultAsync();

        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        return product;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        int targetQuantity = 1000;
        int targetYear = 2025;

        return await _dbContext.Products
                .Where(p => p.ReceivedDate.Year == targetYear && p.Quantity > targetQuantity)
                .ToListAsync();
    }
}
