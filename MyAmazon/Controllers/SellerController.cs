using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAmazon.Data.Repository.Interfaces;
using MyAmazon.DataTransferObjects;
using MyAmazon.Models;
using System.Linq.Expressions;

namespace MyAmazon.Controllers;

[ApiController]
[Route("api/seller")]
public class SellerController : ControllerBase
{
    private IRepositoryWrapper _repoWrapper;
    private IMapper _mapper;
    public SellerController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
    {
        _repoWrapper = repositoryWrapper;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult FindAllSellers()
    {
        System.Diagnostics.Debug.WriteLine("kokok");
        try
        {
            var sellers = _repoWrapper.SellerRepository.GetAll();

            var sellersResult =_mapper.Map<IEnumerable<SellerDTO>>(sellers);
            return Ok(sellersResult);
        }
        catch(Exception ex){
            
            //TODO - add logging
            System.Diagnostics.Debug.WriteLine("kokok" + ex);

            return StatusCode(500, "Internal Server error");
        }
    }

    [HttpGet("{id}", Name = "SellerById")]
    public IActionResult FindSellerById(Guid id)
    {
        try
        {
            var sellerList = _repoWrapper.SellerRepository.FindByCondition(s => s.Id == id).ToList();

            if(sellerList is null) return NotFound();
            else
            {
                var sellerResult = _mapper.Map<SellerDTO>(sellerList[0]);
                return Ok(sellerResult);
            }
        }
        catch (System.Exception ex)
        {
            //TODO - add logging

            return StatusCode(500, "Internal Server error");
        }
    }
    

    [HttpPost]
    public IActionResult CreateSeller([FromBody] SellerCreateDTO seller)
    {
        try
        {
            if(seller is null)
            {
                //TODO logging
                return BadRequest("Seller is null");
            }
            if(!ModelState.IsValid)
            {
                // TODO logging
                return BadRequest("Invalid seller");
            }

            var sellerEntity = _mapper.Map<Seller>(seller);
            _repoWrapper.SellerRepository.Create(sellerEntity);
            _repoWrapper.Save();

            var createdSeller = _mapper.Map<SellerDTO>(sellerEntity);

            return CreatedAtRoute("SellerById", new {id = createdSeller.Id}, createdSeller);
        }
        catch (System.Exception)
        {
            //TODO - add logging

            return StatusCode(500, "Internal Server error");
        }

    }

     [HttpPut]
    public IActionResult UpdateSeller(Guid id, [FromBody] SellerCreateDTO seller)
    {
        try
        {
            if(seller is null)
            {
                //TODO logging
                return BadRequest("Seller is null");
            }
            if(!ModelState.IsValid)
            {
                // TODO logging
                return BadRequest("Invalid seller");
            }

            var sellerEntity = _repoWrapper.SellerRepository.FindByCondition(s => s.Id == id).ToList();
            if(sellerEntity is null || !sellerEntity.Any())
            {
                //TODO - add logging
                return NotFound();
            }

            _mapper.Map(seller, sellerEntity);

            _repoWrapper.SellerRepository.Update(sellerEntity[0]);
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
        public IActionResult DeleteSeller(Guid id)
        {
            try
            {
                var sellerList = _repoWrapper.SellerRepository.FindByCondition(sl => sl.Id == id).ToList();
                if (sellerList == null || !sellerList.Any())
                {
                    // TODO logging
                    return NotFound();
                }

                if (_repoWrapper.ProductRepository.FindByCondition(p => p.SellerId == id).Any())
                {
                    // TODO logging
                    return BadRequest("Cannot delete seller. It has related products.");
                }

                _repoWrapper.SellerRepository.Delete(sellerList[0].Id);
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