namespace MyAmazon.DataTransferObjects;

public class SellerDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public IEnumerable<ProductDTO>? Products { get; set; }
}