import React, { useEffect, useState } from 'react';
import Cookies from 'js-cookie';
import CartPopup from './CartPopup';
import Header from './Header';
import './css/ProductList.css';
import ICartService from "../services/Interfaces/ICartService";

function ProductList() {
    const [products, setProducts] = useState([]);
    const [cartVisible, setCartVisible] = useState(false);
    const [cartItems, setCartItems] = useState({ items: [], total: 0 });
    const [cartMessage, setCartMessage] = useState('');
    const [sortOption, setSortOption] = useState('price-asc');
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    // Fetch products
    useEffect(() => {
        ICartService.getProducts()
            .then((products) => {
                setProducts(products);
            })
            .catch((error) => {
                console.error('Error fetching products:', error);
                setError('Failed to load products.');
            })
            .finally(() => setLoading(false));
    }, []);
    
    // Open Cart Popup
    const openCart = () => {
        const sessionId = Cookies.get('sessionId');
        if (!sessionId) {
            console.error('Session ID not found in cookies.');
            setError('Session ID is missing. Please reload the page.');
            return;
        }

        ICartService.getCart(sessionId)
            .then((response) => {
                console.log('Cart API response:', response); // Log the full response

                // Ensure the response structure matches the state shape
                const cartData = response || { items: [], total: 0 };

                setCartItems(cartData); // Update the state correctly
                console.log('Updated Cart Items:', cartData);

                setCartVisible(true); // Open the cart popup
            })
            .catch((error) => {
                console.error('Error fetching cart:', error.response?.data || error.message);
                setError('Failed to load cart items.');
            });

    };



    // Close Cart Popup
    const closeCart = () => setCartVisible(false);

    // Add Product to Cart
// Add Product to Cart
    const addToCart = (productId) => {
        const sessionId = Cookies.get('sessionId');
        if (!sessionId) {
            console.error('Session ID not found.');
            setCartMessage('Failed to add item to cart.');
            return;
        }

        const cartItem = { productId, quantity: 1, sessionId };

        // Log the product being added
        console.log('Adding product to cart:', cartItem);

        ICartService.addToCart(cartItem)
            .then(() => {
                setCartMessage('Item added to cart!');

                // Fetch the updated cart to sync state
                ICartService.getCart(sessionId)
                    .then((response) => {
                        const updatedCart = response.items || [];
                        setCartItems({
                            items: updatedCart,
                            total: calculateCartTotal(updatedCart),
                        });

                        // Log the updated cart
                        console.log('Updated cart:', updatedCart);
                    })
                    .catch((error) => {
                        console.error('Error fetching updated cart:', error);
                    });

                setTimeout(() => setCartMessage(''), 3000);
            })
            .catch((error) => {
                console.error('Error adding to cart:', error.response?.data || error.message);
                setCartMessage('Failed to add item to cart.');
            });
    };



    const calculateCartTotal = (cartItems) => {
        return cartItems.reduce((total, item) => total + (item.totalPrice || 0), 0);
    };
    
    // Handle sorting
    const handleSortChange = (option) => {
        setSortOption(option);

        const sortedProducts = [...products];
        if (option === 'price-asc') sortedProducts.sort((a, b) => a.price - b.price);
        if (option === 'price-desc') sortedProducts.sort((a, b) => b.price - a.price);
        if (option === 'Alphabetical') sortedProducts.sort((a, b) => a.name.localeCompare(b.name));

        setProducts(sortedProducts);
    };

    // Handle Loading and Errors
    if (loading) return <div>Loading products...</div>;
    if (error) return <div className="error-message">{error}</div>;

    // Render Component
    return (
        <div>
            <Header
                onSortChange={handleSortChange}
                onCartClick={openCart}
                sortOption={sortOption}
                cartItemCount={cartItems.items?.length || 0}
            />
            {cartMessage && <div>{cartMessage}</div>}
            <div className="product-grid">
                {products.map((product) => (
                    <div key={product.id} className="product-card">
                        <h2>{product.name}</h2>
                        <p>{product.price} DKK</p>
                        <button onClick={() => addToCart(product.id)}>Add to Cart</button>
                    </div>
                ))}
            </div>
            {cartVisible && <CartPopup cartItems={cartItems} onClose={closeCart} />}
        </div>
    );
}
export default ProductList;
