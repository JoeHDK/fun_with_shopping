import React from 'react';
import './Header.css'; // For styling

function Header({ onSort, sortOrder }) {
    return (
        <header className="header">
            <h1>Webshop</h1>
            <div className="header-actions">
                {/* Sort Button */}
                <button onClick={onSort} className="sort-button">
                    Sort by Price ({sortOrder === 'asc' ? 'Ascending' : 'Descending'})
                </button>

                {/* Cart Button */}
                <button className="cart-button">
                    🛒 View Cart
                </button>
            </div>
        </header>
    );
}

export default Header;
