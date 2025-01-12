using WebshopApi.Models;
using WebshopApi.Repositories;

namespace WebshopApi.Services;

public class CartService(ICartRepository repository) : ICartService
{
    public IEnumerable<Cart> GetCartItems(string sessionId) =>
        repository.GetAllBySessionId(sessionId);

    public Cart? GetCartItem(int id) => repository.GetById(id);

    public void AddToCart(Cart cartItem)
    {
        var existingItem = repository.GetAllBySessionId(cartItem.SessionId)
            .FirstOrDefault(c => c.ProductId == cartItem.ProductId);

        if (existingItem != null)
        {
            existingItem.Quantity += cartItem.Quantity;
            repository.Update(existingItem);
        }
        else
        {
            repository.Add(cartItem);
        }

        repository.SaveChanges();
    }

    public void RemoveFromCart(int id)
    {
        var cartItem = repository.GetById(id);
        if (cartItem == null) return;

        repository.Delete(cartItem);
        repository.SaveChanges();
    }

    public decimal GetCartTotal(string sessionId)
    {
        var cartItems = repository.GetAllBySessionId(sessionId);
        
        decimal total = 0;
        
        // Calculating potential discount
        foreach (var item in cartItems)
        {
            var itemPrice = item.Product.Price * item.Quantity;
            if (item.Quantity >= 5)
            {
                itemPrice *= (1 - item.Product.Discount / 100);
            }

            total += itemPrice;
        }

        if (total > 5000)
        {
            total *= 0.95m;
        }
        return total;
    }
}