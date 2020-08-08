using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public string GetProducts()
        {
            return "This method will be a list of product";
        }



        [HttpGet("{id}")]
        public string GetProduct()
        {
            return "Single Product";
        }

    }
}