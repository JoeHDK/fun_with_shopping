using Microsoft.EntityFrameworkCore;
using WebshopApi.Data;
using WebshopApi.Models;

namespace WebshopApi.Repositories;

public class ProductRepository(WebshopDbContext context) : IProductRepository
{
    public IEnumerable<Product> GetAll() => context.Products.ToList();

    public Product? GetById(int id) => context.Products.Find(id);

    public void Add(Product product)
    {
        context.Products.Add(product);
    }
    
    public IEnumerable<Product> Search(string query)
    {
        var sanitizedQuery = query.Replace("%", "[%]").Replace("_", "[_]");
        return context.Products
            .Where(p =>
                EF.Functions.Like(p.Name, $"%{sanitizedQuery}%") ||
                EF.Functions.Like(p.Description, $"%{sanitizedQuery}%") ||
                EF.Functions.Like(p.Category, $"%{sanitizedQuery}%"))
            .OrderBy(p => p.Name)
            .Take(10)
            .ToList();
    }

    public void Update(Product product)
    {
        context.Products.Update(product);
    }

    public void Delete(Product product)
    {
        context.Products.Remove(product);
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public bool ProductExists(int id)
    {
        return context.Products.Any(p => p.Id == id);
    }
}