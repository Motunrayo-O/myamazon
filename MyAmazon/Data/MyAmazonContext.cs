using Microsoft.EntityFrameworkCore;
using MyAmazon.Models;

namespace MyAmazon.Data;
public class MyAmazonContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
       options.UseSqlite("DataSource = MyAmazon.db; Cache=Shared");
    //    options.UseSqlite("DataSource = /Users/motunrayoogunyinka/Documents/Training/dotnet/MyAmazon/MyAmazon/MyAmazon.db; Cache=Shared");

    public DbSet<Product> Products { get; set; }
    public DbSet<Seller> Sellers { get; set; }
}