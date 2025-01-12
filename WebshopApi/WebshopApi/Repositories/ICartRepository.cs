using WebshopApi.Models;

namespace WebshopApi.Repositories;

public interface ICartRepository
{
    IEnumerable<Cart> GetAllBySessionId(string sessionId);
    Cart? GetById(int id);
    void Add(Cart cartItem);
    void Update(Cart cartItem);
    void Delete(Cart cartItem);
    void SaveChanges();
}