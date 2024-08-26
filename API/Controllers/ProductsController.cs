using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext context;

        public ProductsController(StoreContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(){
            List<Product>? products = null;
            if(context.Products is not null){
                products = await context.Products.ToListAsync();
            }
            Console.WriteLine(products);
            return Ok(products);
        }

        [HttpGet("{ProductId}")]

        public async Task<ActionResult<Product>> GetProductById(int ProductId){
            Product? product = null; 
            if(context.Products is not null){
                 product = await context.Products.FindAsync(ProductId);
            }
            return product;
        }
    }
}
