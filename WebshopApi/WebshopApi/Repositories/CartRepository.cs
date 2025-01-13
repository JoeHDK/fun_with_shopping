using WebshopApi.Data;
using WebshopApi.Models;

namespace WebshopApi.Repositories;

public class CartRepository(WebshopDbContext context) : ICartRepository
{
    public IEnumerable<Cart> GetAllBySessionId(string sessionId) =>
        context.Cart.Where(c => c.SessionId == sessionId).ToList();

    public Cart? GetById(int id) => context.Cart.Find(id);

    public void Add(Cart cartItem)
    {
        context.Cart.Add(cartItem);
    }

    public void Update(Cart cartItem)
    {
        context.Cart.Update(cartItem);
    }

    public void Delete(Cart cartItem)
    {
        context.Cart.Remove(cartItem);
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }
}