namespace MyAmazon.DataTransferObjects;

public class ProductDTO
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
    public string? Type { get; set; }
    public string? DisplayName { get; set; }
    public string? Brand { get; set; }
    public string? Category { get; set; }
    public bool Active { get; set; }
}