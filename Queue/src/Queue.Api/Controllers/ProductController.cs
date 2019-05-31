using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Queue.Api.Models;
using Queue.Api.Services.Products;

namespace Queue.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/product?query=abc
        [HttpGet]
        public async Task<IActionResult> Get(string query) => Ok(await _productService.SearchAsync(query));

        // GET api/product/123
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _productService.GetAsync(id));
        }

        // POST api/product
        [HttpPost]
        public void Post([FromBody] Product model)
        {
            _productService.Create(model);
        }
    }
}
