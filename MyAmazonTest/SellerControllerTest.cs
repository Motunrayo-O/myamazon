using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
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
    [Ignore]
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
    [Ignore]
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

     [TestMethod]
    public void FindSellerById_BadId_ReturnsNotFound()
    {
        var repoWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = GetMapper();
        var sellerController = new SellerController(repoWrapperMock.Object, mapper);

        var id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950f");
        var result = sellerController.FindSellerById(id) as StatusCodeResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
    }

    [TestMethod]
    public void ValidCreate_CreatesSeller()
    {
        var repoWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = GetMapper();
        var sellerController = new SellerController(repoWrapperMock.Object, mapper);

        var sellerToCreate = new SellerCreateDTO()
        {
            Name = "Jomi Loju"
        };

        var result = sellerController.CreateSeller(sellerToCreate) as ObjectResult;

        Assert.IsInstanceOfType(result, typeof(CreatedAtRouteResult));
        Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
        Assert.AreEqual("SellerById", (result as CreatedAtRouteResult).RouteName);
    }

    [TestMethod]
    public void CreateSeller_NullSeller_ReturnsBadRequest()
    {
        var repoWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = GetMapper();
        var sellerController = new SellerController(repoWrapperMock.Object, mapper);

        var result = sellerController.CreateSeller(null) as ObjectResult;

        Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
    }

    [TestMethod]
    public void UpdateSeller_ValidSeller_Updates()
    {
        var repoWrapperMock = MockIRepositoryWrapper.GetMock().Object;
        var mapper = GetMapper();
        
        var sellerController = new SellerController(repoWrapperMock, mapper);

        var result = sellerController.UpdateSeller(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"), new SellerCreateDTO(){
            Name = "Cher"
        }) as StatusCodeResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, result.StatusCode);
        repoWrapperMock.SellerRepository.Object.Verify(s => s.Update());
    }
}