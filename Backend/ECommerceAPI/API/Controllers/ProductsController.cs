using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Specifications;
using ECommerceAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<ProductType> typeRepo;
        private readonly IGenericRepository<ProductBrand> brandRepo;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> productRepo
            , IGenericRepository<ProductType> typeRepo
            , IGenericRepository<ProductBrand> brandRepo
            , IMapper mapper)
        {
            this.productRepo = productRepo;
            this.typeRepo = typeRepo;
            this.brandRepo = brandRepo;
            this.mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDTO>>> GetProducts(
            [FromQuery]ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductWithFiltersForCountSpecification(productParams);
            var totalItems = await productRepo.CountAsync(countSpec);
            var products = await productRepo.GetAllWithSpecAsync(spec);
            var data = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);


            return Ok(new Pagination<ProductDTO>(productParams.PageIndex,
                productParams.PageSize, totalItems, data));
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await productRepo.GetEntityWithSpec(spec);

            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(mapper.Map<Product, ProductDTO>(product));
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductBrands()
        {
            var productBrands = await brandRepo.GetAllAsync();
            return Ok(productBrands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductTypes()
        {
            var productTypes = await typeRepo.GetAllAsync();
            return Ok(productTypes);
        }

        //// PUT: api/Products/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProduct(int id, Product product)
        //{
        //    if (id != product.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(product).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Products
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Product>> PostProduct(Product product)
        //{
        //    _context.Products.Add(product);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        //}

        //// DELETE: api/Products/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Products.Remove(product);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ProductExists(int id)
        //{
        //    return _context.Products.Any(e => e.Id == id);
        //}
    }
}
