using Core.Interfaces;
using Core.Models;
using ECommerceAPI.Data;
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
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return (IEnumerable<Product>)await context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await context.Products.FindAsync(id);
        }
    }
}
