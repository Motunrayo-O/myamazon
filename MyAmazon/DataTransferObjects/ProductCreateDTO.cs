using System.ComponentModel.DataAnnotations;

namespace MyAmazon.DataTransferObjects;

public class ProductCreateDTO
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
    public string? Name { get; set; }

    [StringLength(200, ErrorMessage = "Name can't be longer than 200 characters")]

    public string? Description { get; set; }
    public string? Type { get; set; }
    public string? DisplayName { get; set; }
    public string? Brand { get; set; }

    [Required(ErrorMessage = "Category is required")]
    public string? Category { get; set; }
    public bool Active { get; set; }
}