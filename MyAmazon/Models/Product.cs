using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAmazon.Models;
public class Product
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(120, ErrorMessage = "Name can't be longer than 120 characters")]
    public string? Description { get; set; }
    public string? Type { get; set; }
    public string? DisplayName { get; set; }
    public string? Brand { get; set; }
    public string? Category { get; set; }
    public bool Active { get; set; }

    [ForeignKey(nameof(Seller))]
    public Guid SellerId { get; set; }
    public Seller? Seller { get; set; }
}