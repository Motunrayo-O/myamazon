using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAmazon.Data.Repository.Interfaces;
using MyAmazon.DataTransferObjects;
using MyAmazon.Models;
using System.Linq.Expressions;

namespace MyAmazon.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{
    private IRepositoryWrapper _repoWrapper;
    private IMapper _mapper;
    public ProductController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
    {
        _repoWrapper = repositoryWrapper;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult FindAllProducts()
    {
        try
        {
            var products = _repoWrapper.ProductRepository.GetAll();

            var productsResult =_mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(productsResult);
        }
        catch(Exception ex){
            
            //TODO - add logging

            return StatusCode(500, "Internal Server error");
        }
    }

    [HttpGet("{id}", Name = "ProductById")]
    public IActionResult FindProductById(Guid id)
    {
        try
        {
            var productList = _repoWrapper.ProductRepository.FindByCondition(s => s.Id == id).ToList();

            if(productList is null) return NotFound();
            else
            {
                var productResult = _mapper.Map<ProductDTO>(productList[0]);
                return Ok(productResult);
            }
        }
        catch (System.Exception ex)
        {
            //TODO - add logging

            return StatusCode(500, "Internal Server error");
        }
    }

    [HttpPost]
    public IActionResult CreateProduct([FromBody] ProductCreateDTO product)
    {
        try
        {
            if(product is null)
            {
                //TODO logging
                return BadRequest("Product is null");
            }
            if(!ModelState.IsValid)
            {
                // TODO logging
                return BadRequest("Invalid product");
            }

            var productEntity = _mapper.Map<Product>(product);
            _repoWrapper.ProductRepository.Create(productEntity);
            _repoWrapper.Save();

            var createdProduct = _mapper.Map<ProductDTO>(productEntity);

            return CreatedAtRoute("ProductById", new {id = createdProduct.Id}, createdProduct);
        }
        catch (System.Exception)
        {
            //TODO - add logging

            return StatusCode(500, "Internal Server error");
        }

    }

     [HttpPut]
    public IActionResult UpdateProduct(Guid id, [FromBody] ProductCreateDTO product)
    {
        try
        {
            if(product is null)
            {
                //TODO logging
                return BadRequest("Product is null");
            }
            if(!ModelState.IsValid)
            {
                // TODO logging
                return BadRequest("Invalid product");
            }

            var productEntity = _repoWrapper.ProductRepository.FindByCondition(s => s.Id == id).ToList();
            if(productEntity is null || !productEntity.Any())
            {
                //TODO - add logging
                return NotFound();
            }

            _mapper.Map(product, productEntity);

            _repoWrapper.ProductRepository.Update(productEntity[0]);
            _repoWrapper.Save();

            return NoContent();
        }
        catch (System.Exception)
        {
            //TODO - add logging

            return StatusCode(500, "Internal Server error");
        }

    }


        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            try
            {
                var productList = _repoWrapper.ProductRepository.FindByCondition(sl => sl.Id == id).ToList();
                if (productList == null || !productList.Any())
                {
                    // TODO logging
                    return NotFound();
                }

                if (_repoWrapper.ProductRepository.FindByCondition(p => p.Id == id).Any())
                {
                    // TODO logging
                    return BadRequest("Cannot delete product. It has related products. Delete those products first");
                }

                _repoWrapper.ProductRepository.Delete(productList[0].Id);
                _repoWrapper.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                // TODO logging
                return StatusCode(500, "Internal server error");
            }
        }
    
    

}