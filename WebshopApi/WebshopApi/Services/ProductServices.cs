using WebshopApi.Models;
using WebshopApi.Repositories;

namespace WebshopApi.Services;
public class ProductService(IProductRepository productRepository) : IProductService
{
    public IEnumerable<Product> GetAllProducts() => productRepository.GetAll().ToList();

    public Product? GetProductById(int id) => productRepository.GetById(id);

    public void AddProduct(Product product)
    {
        productRepository.Add(product);
        productRepository.SaveChanges();
    }
    
    public IEnumerable<Product> SearchProducts(string query)
    {
        query = query.Trim();
        if (query.Length > 100)
        {
            throw new ArgumentException("Search query is too long.");
        }

        return productRepository.Search(query).Take(10);
    }

    public void UpdateProduct(int id, Product product)
    {
        var existing = productRepository.GetById(id);
        if (existing == null) return;
        existing.Name = product.Name;
        existing.Description = product.Description;
        existing.Price = product.Price;
        existing.Category = product.Category;
        existing.Discount = product.Discount;
        
        productRepository.Update(existing);
        productRepository.SaveChanges();
    }

    public void DeleteProduct(int id)
    {
        var product = productRepository.GetById(id);
        if (product == null) return;
        
        productRepository.Delete(product);
        productRepository.SaveChanges();
    }
}