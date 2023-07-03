using Moq;
using MyAmazon.Data.Repository.Interfaces;

namespace MyAmazonTest.Mocks;

internal class MockIRepositoryWrapper
{

    public static Mock<IRepositoryWrapper> GetMock()
    {
        var mock = new Mock<IRepositoryWrapper>();
        var productMock = MockIProductRepository.GetMock();
        var sellerMock = MockISellerRepository.GetMock();

        mock.Setup(m => m.ProductRepository).Returns(() =>  productMock.Object);
        mock.Setup(m => m.SellerRepository).Returns(() => sellerMock.Object);
        mock.Setup(m => m.Save()).Callback(() => { return; });

        return mock;
    }
}
