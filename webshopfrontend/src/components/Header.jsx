import React from 'react';
import './css/Header.css';

function Header({ onSortChange, onCartClick, sortOption, cartItemCount }) {
    const cartItems = cartItemCount ? cartItemCount : [];
    return (
        <header className="header">
            <h1>Webshop</h1>
            <div className="header-actions">
                {/* Sort Dropdown */}
                <select value={sortOption} onChange={(e) => onSortChange(e.target.value)}>
                    <option value="price-asc">Price (Ascending)</option>
                    <option value="price-desc">Price (Descending)</option>
                    <option value="Alphabetical">Alphabetical</option>
                </select>

                {/* Cart Button */}
                <button
                    className={`cart-button`}
                    onClick={onCartClick}
                >
                    🛒 
                </button>
            </div>
        </header>
    );
}

export default Header;
