using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext context;
        public ProductRepository(StoreContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<ProductBrand>> GetAllProductBrandsAsync()
        {
            return await context.ProductBrands.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await context.Products
                .Include(p=>p.ProductBrand)
                .Include(p=> p.ProductType)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetAllProductTypesAsync()
        {
            return await context.ProductTypes.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await context.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}
