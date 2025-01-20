using WebshopApi.Models;

namespace WebshopApi.Repositories;
public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product? GetById(int id);
    void Add(Product product);
    IEnumerable<Product> Search(string query);
    void Update(Product product);
    void Delete(Product product);
    void SaveChanges();
    bool ProductExists(int id);
}