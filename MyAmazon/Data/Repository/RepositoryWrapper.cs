using MyAmazon.Models;
using MyAmazon.Data.Repository.Interfaces;

namespace MyAmazon.Data.Repository;

public class RepositoryWrapper : IRepositoryWrapper
{
    private MyAmazonContext _repoContext;
    private IProductRepository _productRepo;
    private ISellerRepository _sellerRepo;

    public RepositoryWrapper(MyAmazonContext context){
        _repoContext = context;
    }

    public IProductRepository ProductRepository
    {
        get
        {
            if( _productRepo == null)
            {
                _productRepo = new ProductRepository(_repoContext);
            }

            return _productRepo;
        }
    }

    public ISellerRepository SellerRepository 
    {
        get
        {
            if( _sellerRepo == null)
            {
                _sellerRepo = new SellerRepository(_repoContext);
            }

            return _sellerRepo;
        }
    }

    public void Save()
    {
        _repoContext.SaveChanges();
    }
}