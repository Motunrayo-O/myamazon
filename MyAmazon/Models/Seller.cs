using System.ComponentModel.DataAnnotations;

namespace MyAmazon.Models;
public class Seller
{
    [Key]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
    public string? Name { get; set; }

    public ICollection<Product>? Products {get;set;}
}
