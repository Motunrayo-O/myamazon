using System.Linq.Expressions;
using Moq;
using MyAmazon.Data.Repository.Interfaces;
using MyAmazon.Models;

namespace MyAmazonTest.Mocks;

internal class MockIProductRepository
{
    public static Mock<IProductRepository> GetMock()
    {
        var mock = new Mock<IProductRepository>();

        var product2 = new Product()
        {
            Id = Guid.Parse("8f8fad5b-d9cb-469f-a165-70867728950e"),
            Name = "Blue Jeans",
            SellerId = Guid.Parse("1f8fad5b-d9cb-469f-a165-70867728950e")
        };

        List<Product> productList = new List<Product>();
        productList.Add(product2);
        IQueryable<Product> queryableProducts = productList.AsQueryable();

        var products = new List<Product>()
        {
            new Product()
            {
                Id = Guid.Parse("7f8fad5b-d9cb-469f-a165-70867728950e"),
                Name = "Dami Smith",
                SellerId = Guid.Parse("1f8fad5b-d9cb-469f-a165-70867728950e")
            },
            product2
        };

    IQueryable<Product> allProducts = products.AsQueryable();

    mock.Setup(mock => mock.GetAll()).Returns(() => products);
    mock.Setup(mock => mock.FindByCondition(It.IsAny<Expression<Func<Product, bool>>>())).Returns(() => queryableProducts);
    mock.Setup(mock => mock.Create(It.IsAny<Product>())).Callback(() => { return;});
    mock.Setup(mock => mock.Update(It.IsAny<Product>())).Callback(() => { return;});
    mock.Setup(mock => mock.Delete(It.IsAny<Guid>())).Callback(() => { return;});

        return mock;
    }
}