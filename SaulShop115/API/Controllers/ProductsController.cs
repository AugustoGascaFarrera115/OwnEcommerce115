using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;

namespace API.Controllers
{
    /*[ApiController]
    [Route("api/[controller]")]*/
    public class ProductsController : BaseApiController
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

        public IMapper mapper { get; }

        public ProductsController(IGenericRepository<Product> productsRepo,IGenericRepository<ProductBrand> productBrandsRepo,IGenericRepository<ProductType> productTypesRepo,
        IMapper mapper)
        {
            this.productsRepo = productsRepo;
            this.productBrandsRepo = productBrandsRepo;
            this.productTypesRepo = productTypesRepo;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
        {
            //var products = await repo.GetProductsAsync();

            var spec = new ProductsWithTypesAndBrandsSpecifications();

            //var products = await productsRepo.ListAllAsync();

            var products = await productsRepo.ListAsync(spec);

            //return Ok(products);

          /* return products.Select(product => new ProductToReturnDto{

                    
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PictureUrl = product.PictureUrl,
                Price = product.Price,
                ProductBrand = product.ProductBrand.Name,
                ProductType = product.ProductType.Name

            }).ToList(); */

            return Ok(mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            //return await productsRepo.GetByIdAsync(id);
            var spec = new ProductsWithTypesAndBrandsSpecifications(id);
            var product = await productsRepo.GetEntityWithSpecification(spec);

           /* return new ProductToReturnDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PictureUrl = product.PictureUrl,
                Price = product.Price,
                ProductBrand = product.ProductBrand.Name,
                ProductType = product.ProductType.Name

            }; */

            return mapper.Map<Product,ProductToReturnDto>(product);

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