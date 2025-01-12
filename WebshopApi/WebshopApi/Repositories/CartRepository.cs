using WebshopApi.Data;
using WebshopApi.Models;

namespace WebshopApi.Repositories;

public class CartRepository(WebshopDbContext context) : ICartRepository
{
    public IEnumerable<Cart> GetAllBySessionId(string sessionId) =>
        context.Carts.Where(c => c.SessionId == sessionId).ToList();

    public Cart? GetById(int id) => context.Carts.Find(id);

    public void Add(Cart cartItem)
    {
        context.Carts.Add(cartItem);
    }

    public void Update(Cart cartItem)
    {
        context.Carts.Update(cartItem);
    }

    public void Delete(Cart cartItem)
    {
        context.Carts.Remove(cartItem);
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }
}