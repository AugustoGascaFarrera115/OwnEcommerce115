using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {


       // private readonly StoreContext _context;
       //private readonly IProductRepository repo;
        /*public ProductsController(IProductRepository repo)
        {
            this.repo = repo;
        } */
        private readonly IGenericRepository<Product> productsRepo;
        private readonly IGenericRepository<ProductBrand> productBrandsRepo;

        private readonly IGenericRepository<ProductType> productTypesRepo;

        public ProductsController(IGenericRepository<Product> productsRepo,IGenericRepository<ProductBrand> productBrandsRepo,IGenericRepository<ProductType> productTypesRepo)
        {
            this.productsRepo = productsRepo;
            this.productBrandsRepo = productBrandsRepo;
            this.productTypesRepo = productTypesRepo;
        }


        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            //var products = await repo.GetProductsAsync();

            var spec = new ProductsWithTypesAndBrandsSpecifications();

            //var products = await productsRepo.ListAllAsync();

            var products = await productsRepo.ListAsync(spec);

            return Ok(products);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            //return await productsRepo.GetByIdAsync(id);
            var spec = new ProductsWithTypesAndBrandsSpecifications(id);
            return await productsRepo.GetEntityWithSpecification(spec);

        }


        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await productBrandsRepo.ListAllAsync());
        }

        
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await productTypesRepo.ListAllAsync());
        }

    }
}