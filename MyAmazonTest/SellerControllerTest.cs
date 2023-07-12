using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAmazon;
using MyAmazon.Controllers;
using MyAmazon.DataTransferObjects;
using MyAmazonTest.Mocks;
// using MyAmazon.Controllers;

namespace MyAmazonTest;

[TestClass]
public class SellerControllerTest
{
    public IMapper GetMapper()
    {
        var mappingProfile = new MappingProfile();
        var config = new MapperConfiguration(c => c.AddProfile(mappingProfile));
        return new Mapper(config);
    }

    [TestMethod]
    public void FindAllSellers_ReturnsAllSellers()
    {
        var repoWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = GetMapper();
        var sellerController = new SellerController(repoWrapperMock.Object, mapper);

        var result = sellerController.FindAllSellers() as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<SellerDTO>));
        Assert.IsTrue((result.Value as IEnumerable<SellerDTO>).Any());
    }

    [TestMethod]
    public void FindSellerById_ReturnsSeller()
    {
        var repoWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = GetMapper();
        var sellerController = new SellerController(repoWrapperMock.Object, mapper);

        var id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");
        var result = sellerController.FindSellerById(id) as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        Assert.IsInstanceOfType(result.Value, typeof(SellerDTO));
    }
}