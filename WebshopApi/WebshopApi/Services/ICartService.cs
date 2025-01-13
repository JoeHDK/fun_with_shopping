using WebshopApi.DTOs;
using WebshopApi.Models;

namespace WebshopApi.Services;

public interface ICartService
{
    IEnumerable<Cart> GetCartItems(string sessionId);
    Cart? GetCartItem(int id);
    //void AddToCart(Cart cartItem);
    void AddToCart(int productId, int quantity, string sessionId);
    void RemoveFromCart(int id);
    decimal GetCartTotal(string sessionId);
}