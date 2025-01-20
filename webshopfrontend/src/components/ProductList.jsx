import React, { useEffect, useState } from 'react';
import CartService from '../services/CartService'; // Consistent service usage
import Cookies from 'js-cookie';
import CartPopup from './CartPopup'; // Popup component for cart details
import './css/ProductList.css';

function ProductList() {
    const [products, setProducts] = useState([]);
    const [selectedProduct, setSelectedProduct] = useState(null);
    const [cartVisible, setCartVisible] = useState(false);
    const [cartItems, setCartItems] = useState([]);
    const [cartMessage, setCartMessage] = useState('');
    const [sortOption, setSortOption] = useState('price-asc');
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    // Fetch products
    useEffect(() => {
        CartService.getProducts()
            .then((response) => {
                setProducts(response.data);
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

        CartService.getCart(sessionId)
            .then((response) => {
                setCartItems(response.data); // Assuming response.data is an array
                setCartVisible(true);
            })
            .catch((error) => {
                console.error('Error fetching cart:', error.response?.data || error.message);
                setError('Failed to load cart items.');
            });
    };

    // Close Cart Popup
    const closeCart = () => setCartVisible(false);

    // Add Product to Cart
    const addToCart = (productId) => {
        const sessionId = Cookies.get('sessionId');
        if (!sessionId) {
            console.error('Session ID not found.');
            setCartMessage('Failed to add item to cart.');
            return;
        }

        const cartItem = { productId, quantity: 1, sessionId };
        CartService.addToCart(cartItem)
            .then(() => {
                setCartMessage('Item added to cart!');
                setTimeout(() => setCartMessage(''), 3000);
            })
            .catch((error) => {
                console.error('Error adding to cart:', error);
                setCartMessage('Failed to add item to cart.');
            });
    };

    // Sort Products
    const handleSortChange = (event) => {
        const option = event.target.value;
        setSortOption(option);

        const sortedProducts = [...products];
        if (option === 'price-asc') sortedProducts.sort((a, b) => a.price - b.price);
        if (option === 'price-desc') sortedProducts.sort((a, b) => b.price - a.price);

        setProducts(sortedProducts);
    };


    // Handle Loading and Errors
    if (loading) return <div>Loading products...</div>;
    if (error) return <div className="error-message">{error}</div>;

    // Render Component
    return (
        <div>
            <header className="header">
                <h1>Webshop</h1>
                <div className="header-actions">
                    <select value={sortOption} onChange={handleSortChange}>
                        <option value="price-asc">Price (Ascending)</option>
                        <option value="price-desc">Price (Descending)</option>
                    </select>
                    <button onClick={openCart}>🛒 View Cart</button>
                </div>
            </header>
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
            {cartVisible && <CartPopup cartItems={cartItems} onClose={closeCart}/>}
        </div>
    );
}

export default ProductList;
