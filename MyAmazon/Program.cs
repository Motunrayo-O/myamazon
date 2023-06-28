using MyAmazon.Data;
using MyAmazon.Data.Repository;
using MyAmazon.Data.Repository.Interfaces;
using MyAmazon.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<MyAmazonContext>();
builder.Services.AddTransient<IGenericRepository<Product>, GenericRepository<Product>>();
builder.Services.AddTransient<IGenericRepository<Seller>, GenericRepository<Seller>>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#region Product API

app.MapGet("myamazon/product/getAll", (IGenericRepository<Product> service) =>
{
    var products = service.GetAll();
    return Results.Ok(products);
})
.WithName("Getmyamazon")
.WithOpenApi();

app.MapGet("myamazon/product/getById", (IGenericRepository<Product> service, Guid id) =>
{
    var products = service.GetById(id);
    return Results.Ok(products);
})
.WithName("GetmyamazonById")
.WithOpenApi();

app.MapPost("myamazon/product/create", (IGenericRepository<Product> service, Product product) =>
{
    service.Create(product);
    service.Save();
    return Results.Ok();
})
.WithName("Createmyamazon")
.WithOpenApi();

app.MapPut("myamazon/product/update", (IGenericRepository<Product> service, Product product) =>
{
    service.Update(product);
    service.Save();
    return Results.Ok();
})
.WithName("Updatemyamazon")
.WithOpenApi();

app.MapDelete("myamazon/product/delete", (IGenericRepository<Product> service, Guid id) =>
{
    service.Delete(id);
    service.Save();
    return Results.Ok();
})
.WithName("Deletemyamazon")
.WithOpenApi();

#endregion

#region Seller API

app.MapGet("myamazon/seller/getAll", (IGenericRepository<Seller> service) =>
{
    var products = service.GetAll();
    return Results.Ok(products);
})
.WithName("GetSeller")
.WithOpenApi();

app.MapGet("myamazon/seller/getById", (IGenericRepository<Seller> service, Guid id) =>
{
    var products = service.GetById(id);
    return Results.Ok(products);
})
.WithName("GetSellerById")
.WithOpenApi();

app.MapPost("myamazon/seller/create", (IGenericRepository<Seller> service, Seller seller) =>
{
    service.Create(seller);
    service.Save();
    return Results.Ok();
})
.WithName("CreateSeller")
.WithOpenApi();

app.MapPut("myamazon/seller/update", (IGenericRepository<Seller> service, Seller seller) =>
{
    service.Update(seller);
    service.Save();
    return Results.Ok();
})
.WithName("UpdateSeller")
.WithOpenApi();

app.MapDelete("myamazon/seller/delete", (IGenericRepository<Seller> service, Guid id) =>
{
    service.Delete(id);
    service.Save();
    return Results.Ok();
})
.WithName("DeleteSeller")
.WithOpenApi();

#endregion

app.Run();
