using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using Moq;
using MyAmazon.Data.Repository.Interfaces;
using MyAmazon.Models;

namespace MyAmazonTest.Mocks;

internal class MockISellerRepository
{
    public static Mock<ISellerRepository> GetMock()
    {
        var mock = new Mock<ISellerRepository>();

        var sellers = new List<Seller>()
        {
            new Seller()
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                Name = "Dami Smith",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Nike Trainers",
                        Category = "Footwear",
                        Description = "Great shoes at a great price!",
                        Brand = "Nike"
                    }
                }
            },
            new Seller()
            {
                Id = Guid.Parse("1f8fad5b-d9cb-469f-a165-70867728950e"),
                Name = "Funmi Belo",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Gold Earrings",
                        Category = "Jewellery",
                        Description = "Great Jewels at a great price!",
                        Brand = "Prada"
                    }
                }
            }
        };

    IQueryable<Seller> allSellers = sellers.AsQueryable();

    mock.Setup(mock => mock.GetAll()).Returns(() => sellers);

    mock.Setup(mock => mock.FindByCondition(It.IsAny<Expression<Func<Seller, bool>>>()))
              .Returns<Expression<Func<Seller, bool>>>(q =>
                  {
                      var query = q.Compile();
                      List<Seller> sellerList = new List<Seller>();
                      var result = sellers.FirstOrDefault(query);
                      if(result != null)
                      {
                        sellerList.Add(result);
                      }
                      return sellerList.AsQueryable();
                  });
    mock.Setup(mock => mock.Create(It.IsAny<Seller>())).Callback(() => { return;});
    mock.Setup(mock => mock.Update(It.IsAny<Seller>())).Callback(() => { return;});
    mock.Setup(mock => mock.Delete(It.IsAny<Guid>())).Callback(() => { return;});

        return mock;
    }
}