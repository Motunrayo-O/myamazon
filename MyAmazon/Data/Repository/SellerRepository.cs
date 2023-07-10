using MyAmazon.Models;
using MyAmazon.Data.Repository.Interfaces;

namespace MyAmazon.Data.Repository;

public class SellerRepository : GenericRepository<Seller>, ISellerRepository
{
    public SellerRepository(MyAmazonContext repositoryContext)
        :base(repositoryContext)
    {
    }
}