using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAmazon.Data.Repository.Interfaces;
using MyAmazon.DataTransferObjects;
using MyAmazon.Models;

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
        try
        {
            var sellers = _repoWrapper.SellerRepository.FindAll();

            var sellersResult =_mapper.Map<IEnumerable<SellerDTO>>(sellers);
            return Ok(sellersResult);
        }
        catch(Exception ex){
            
            //TODO - add logging

            return StatusCode(500, "Internal Server error");
        }
    }

}