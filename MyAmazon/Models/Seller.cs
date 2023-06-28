using System.ComponentModel.DataAnnotations;

namespace MyAmazon.Models;
public class Seller
{
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; }
}
