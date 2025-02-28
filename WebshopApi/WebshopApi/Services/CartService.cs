﻿using WebshopApi.Models;
using WebshopApi.Repositories;

namespace WebshopApi.Services;

public class CartService(ICartRepository cartRepository, IProductRepository productRepository) : ICartService
{
    public IEnumerable<Cart> GetCartItems(string sessionId)
    {
        return cartRepository.GetAllBySessionId(sessionId);
    }

    public Cart? GetCartItem(int id)
    {
        return cartRepository.GetById(id);
    }

    public void AddToCart(int productId, int quantity, string sessionId)
    {
        if (!productRepository.ProductExists(productId))
        {
            throw new ArgumentException("Invalid product ID.");
        }
        //Console.WriteLine($"AddToCart called with ProductId={productId}, Quantity={quantity}, SessionId={sessionId}");

        try
        {
            // Check if the product exists
            //Console.WriteLine($"Checking if product with ID {productId} exists...");
            if (productRepository.GetById(productId) == null)
            {
                //Console.WriteLine($"Product with ID {productId} does not exist.");
                throw new ArgumentException("Invalid product ID.");
            }

            //Console.WriteLine($"Product with ID {productId} exists.");

            // Check if the item is already in the cart
            //Console.WriteLine($"Checking if cart item exists for ProductId={productId} and SessionId={sessionId}...");
            var existingCartItem = cartRepository.GetAllBySessionId(sessionId)
                .FirstOrDefault(c => c.ProductId == productId);

            if (existingCartItem != null)
            {
                //Console.WriteLine($"Cart item found for ProductId={productId}. Updating quantity...");
                // Update the quantity
                existingCartItem.Quantity += quantity;
                cartRepository.Update(existingCartItem);
                //Console.WriteLine($"Updated quantity to {existingCartItem.Quantity} for ProductId={productId}.");
            }
            else
            {
                //Console.WriteLine($"No existing cart item found for ProductId={productId}. Adding new item...");
                // Add a new cart item
                var newCartItem = new Cart
                {
                    ProductId = productId,
                    Quantity = quantity,
                    SessionId = sessionId
                };

                cartRepository.Add(newCartItem);
                Console.WriteLine($"Added new cart item for ProductId={productId} with Quantity={quantity}.");
            }

            // Save changes
            Console.WriteLine("Saving changes to the cart...");
            cartRepository.SaveChanges();
            Console.WriteLine("Changes saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in AddToCart: {ex.Message}");
            throw;
        }
    }


    public void RemoveFromCart(int id)
    {
        var cartItem = cartRepository.GetById(id);
        if (cartItem == null) return;

        cartRepository.Delete(cartItem);
        cartRepository.SaveChanges();
    }

    public decimal GetCartTotal(string sessionId)
    {
        var cartItems = cartRepository.GetAllBySessionId(sessionId);
        var total = cartItems.Sum(item =>
        {
            var basePrice = item.Product.Price * item.Quantity;
            return item.Quantity >= 5
                ? basePrice * (1 - item.Product.Discount / 100)
                : basePrice;
        });

        // Apply global discount if total exceeds 5000 DKK
        if (total > 5000) total *= 0.95m;

        return total;
    }

    public IEnumerable<Cart> GetCartBySessionId(string sessionId)
    {
        return cartRepository.GetAllBySessionId(sessionId);
    }
}