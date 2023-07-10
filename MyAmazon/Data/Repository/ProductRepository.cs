using MyAmazon.Models;
using MyAmazon.Data.Repository.Interfaces;

namespace MyAmazon.Data.Repository;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(MyAmazonContext repositoryContext)
        :base(repositoryContext)
    {
    }
}