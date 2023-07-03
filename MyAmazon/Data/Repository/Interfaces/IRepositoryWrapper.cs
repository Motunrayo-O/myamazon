namespace MyAmazon.Data.Repository.Interfaces;

public interface IRepositoryWrapper
{
    IProductRepository ProductRepository { get; }
    ISellerRepository SellerRepository { get; }
    void Save();
}