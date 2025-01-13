import React, { useEffect, useState } from 'react';
import api from '../services/Api';
import './css/ProductList.css';
import Cookies from 'js-cookie';

function ProductList() {
    const [products, setProducts] = useState([]);
    const [selectedProduct, setSelectedProduct] = useState(null); // Track selected product
    const [sortOption, setSortOption] = useState('price-asc'); // Default sort option
    const [cartMessage, setCartMessage] = useState(''); // Feedback message for cart actions
    const [loading, setLoading] = useState(true); // Loading state for products
    const [error, setError] = useState(null); // Error state for fetching products
    
    useEffect(() => {
        console.log('Fetching products...');
        api.get('/products')
            .then((response) => {
                console.log('Products fetched successfully:', response.data);
                setProducts(response.data);
            })
            .catch((error) => {
                console.error('Error fetching products:', error);
                setError('Failed to load products. Please try again later.');
            })
            .finally(() => {
                setLoading(false);
            });
    }, []);


    const openPopup = (product) => {
        setSelectedProduct(product);
    };

    const closePopup = () => {
        setSelectedProduct(null);
    };

    const handleSortChange = (event) => {
        const option = event.target.value;
        setSortOption(option);

        let sortedProducts = [...products];
        switch (option) {
            case 'price-asc':
                sortedProducts.sort((a, b) => a.price - b.price);
                break;
            case 'price-desc':
                sortedProducts.sort((a, b) => b.price - a.price);
                break;
            case 'alpha-asc':
                sortedProducts.sort((a, b) => a.name.localeCompare(b.name));
                break;
            default:
                break;
        }
        setProducts(sortedProducts);
    };

    const addToCart = (productId) => {
        // Retrieve the session ID from the cookie
        const sessionId = Cookies.get('sessionId');

        if (!sessionId) {
            console.error('Session ID is missing. Ensure session initialization is working correctly.');
            setCartMessage('Failed to add item to cart. No session ID found.');
            return;
        }

        const cartItem = {
            productId, // Matches backend property name
            quantity: 1, // Default quantity
            sessionId, // Use session ID from the cookie
        };

        console.log('Cart item being sent:', cartItem);

        api.post('/cart', cartItem)
            .then(() => {
                setCartMessage('Item added to cart!');
                setTimeout(() => setCartMessage(''), 3000); // Clear message after 3 seconds
            })
            .catch((error) => {
                console.error('Error adding to cart:', error.response?.data || error.message);
                setCartMessage('Failed to add item to cart.');
                setTimeout(() => setCartMessage(''), 3000); // Clear message after 3 seconds
            });
    };

    if (loading) return <div>Loading products...</div>;
    if (error) return <div className="error-message">{error}</div>;

    return (
        <div>
            {/* Header Bar */}
            <header className="header">
                <h1>Webshop</h1>
                <div className="header-actions">
                    {/* Sort Dropdown */}
                    <select
                        value={sortOption}
                        onChange={handleSortChange}
                        className="sort-dropdown"
                    >
                        <option value="price-asc">Price (Ascending)</option>
                        <option value="price-desc">Price (Descending)</option>
                        <option value="alpha-asc">Alphabetical (A-Z)</option>
                    </select>

                    {/* Cart Button Placeholder */}
                    <button className="cart-button">🛒 View Cart</button>
                </div>
            </header>

            {/* Feedback Message */}
            {cartMessage && <div className="cart-message">{cartMessage}</div>}

            {/* Product Grid */}
            <h2>Available Products</h2>
            <div className="product-grid">
                {products.map((product) => (
                    <div
                        key={product.id}
                        className="product-card"
                        onClick={() => openPopup(product)}
                    >
                        <h2>{product.name}</h2>
                        <img
                            src={product.imageUrl}
                            alt={product.name}
                            className="product-image"
                        />
                        <p>
                            <strong>Price:</strong> {product.price} DKK
                        </p>
                    </div>
                ))}
            </div>

            {/* Popup */}
            {selectedProduct && (
                <div className="popup-overlay" onClick={closePopup}>
                    <div
                        className="popup-content"
                        onClick={(e) => e.stopPropagation()} // Prevent closing on content click
                    >
                        <img
                            src={selectedProduct.imageUrl}
                            alt={selectedProduct.name}
                            className="popup-image"
                        />
                        <h2>{selectedProduct.name}</h2>
                        <p>{selectedProduct.description}</p>
                        <p>
                            <strong>Price:</strong> {selectedProduct.price} DKK
                        </p>
                        <button
                            className="buy-button"
                            onClick={() => addToCart(selectedProduct.id)}
                        >
                            Buy
                        </button>
                        <button className="close-button" onClick={closePopup}>
                            Close
                        </button>
                    </div>
                </div>
            )}
        </div>
    );
}

export default ProductList;
