using AutoMapper;
using MyAmazon.DataTransferObjects;
using MyAmazon.Models;

namespace MyAmazon;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Seller, SellerDTO>();
        CreateMap<Product, ProductDTO>();
        CreateMap<SellerCreateDTO, Seller>();
        CreateMap<ProductCreateDTO, Product>();
    }
}