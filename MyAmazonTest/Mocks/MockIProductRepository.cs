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

        var Products = new List<Product>()
        {
            new Product()
            {
                Id = Guid.Parse("7f8fad5b-d9cb-469f-a165-70867728950e"),
                Name = "Dami Smith",
                SellerId = Guid.Parse("1f8fad5b-d9cb-469f-a165-70867728950e")
            },
            product2
        };

    IQueryable<Product> allProducts = Products.AsQueryable();

    mock.Setup(mock => mock.FindAll()).Returns(() => allProducts);
    mock.Setup(mock => mock.FindByCondition(It.IsAny<Expression<Func<Product, bool>>>())).Returns(() => product2);
    mock.Setup(mock => mock.Create(It.IsAny<Product>())).Callback(() => { return;});
    mock.Setup(mock => mock.Update(It.IsAny<Product>())).Callback(() => { return;});
    mock.Setup(mock => mock.Delete(It.IsAny<Product>())).Callback(() => { return;});

        return mock;
    }
}