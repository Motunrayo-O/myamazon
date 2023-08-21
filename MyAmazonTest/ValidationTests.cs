using System.ComponentModel.DataAnnotations;
using MyAmazon.DataTransferObjects;

namespace MyAmazonTest;

[TestClass]
public class ValidationTests
{
    private bool ValidateModel(object model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model, null, null);

        return Validator.TryValidateObject(model, context, results, true);
    }

    [TestMethod]
    [DataRow(null, false)]
    [DataRow("", false)]
    [DataRow(" ", false)]
    [DataRow("John Legend", true)]
    [DataRow("John LegendLegendLegendLegendLegendLegendLegendLegendLegendLegend", false)]
    public void TestModelValidation(string? name, bool isValid)
    {
        var seller = new SellerCreateDTO()
        {
            Name = name
        };

        Assert.AreEqual(isValid, ValidateModel(seller));
    }
}