using WebshopApi.Models;
namespace WebshopApi.Services;
public interface IProductService
{
    IEnumerable<Product> GetAllProducts();
    Product? GetProductById(int id);
    IEnumerable<Product> SearchProducts(string query);
    void AddProduct(Product product);
    void UpdateProduct(int id, Product product);
    void DeleteProduct(int id);
}