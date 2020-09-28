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
using API.Error;
using Microsoft.AspNetCore.Http;
using API.Helpers;

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
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
            //var products = await repo.GetProductsAsync();

            var spec = new ProductsWithTypesAndBrandsSpecifications(productParams);

            var countSpec = new ProductWithFiltersForCountSpecification(productParams);

            var totalItems = await productsRepo.CountAsync(countSpec);

            //var products = await productsRepo.ListAllAsync();

            var products = await productsRepo.ListAsync(spec);


            var data = mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products);

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

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,productParams.PageSize,totalItems,data));
        }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
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

            if(product == null)
            {
                return NotFound(new ApiResponse(404));
            }

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