using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext _context;

    public ProductRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
    {
        return await _context.productBrands.ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        Product? product = null;
        if (_context.Products is not null)
        {
            product = await _context.Products.Include(p => p.ProductType).Include(p => p.ProductBrand).FirstOrDefaultAsync(p => p.id == id);
        }
        return product;
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        List<Product>? products = null;


        if (_context.Products is not null)
        {
            products = await _context.Products.Include(p => p.ProductType).Include(p => p.ProductBrand).ToListAsync();
        }
        return products;
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
    {
        return await _context.ProductTypes.ToListAsync();
    }
}
