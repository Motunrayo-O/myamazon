using MyAmazon.Models;
using MyAmazon.Data.Repository.Interfaces;

namespace MyAmazon.Data.Repository;

public class SellerRepository : GenericRepositoryBase<Seller>, ISellerRepository
{
    public SellerRepository(MyAmazonContext repositoryContext)
        :base(repositoryContext)
    {
    }
}