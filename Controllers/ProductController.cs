using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseAPI.Data;
using PurchaseAPI.Models;
using PurchaseAPI.Models.DTO;
using PurchaseAPI.Models.Mapper;
using PurchaseAPI.Utility;

namespace PurchaseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly PurchaseDbContext _context;

        public ProductController(ILogger<ProductController> logger, PurchaseDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("all")]
        public async Task<ActionResult<Product>> GetAllProducts()
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var products = await _context.Products.Include(p => p.UnitOfMeasure).ToListAsync();
                    await transaction.CommitAsync();
                    return products == null ? NotFound(products) : Ok(products);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var product = await _context.Products.Include(p => p.UnitOfMeasure).Where(p => p.Id == id).FirstOrDefaultAsync();
                    await transaction.CommitAsync();

                    return product == null ? NotFound(product) : Ok(product);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("insert_product")]
        public async Task<ActionResult<Product>> PostProduct(ProductDto productDto)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var productMapper = new ProductMapper();
                    Product product = productMapper.ProductDtoToProduct(productDto);

                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut("update_product/{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (id != product.Id)
                        return BadRequest("Product ID mismatch!");

                    Product? productToUpdate = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                    if (productToUpdate == null)
                        return NotFound("Product is not found!");

                    product.PassValues(ref productToUpdate);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetProductById), new { id = productToUpdate.Id }, productToUpdate);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpDelete("delete_product/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Product? productToDelete = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                    if (productToDelete == null)
                        return NotFound("Product is not found!");

                    _context.Products.Remove(productToDelete);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}