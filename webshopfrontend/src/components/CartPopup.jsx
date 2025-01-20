import './css/CartPopup.css';

function CartPopup({ cartItems, onClose }) {
    if (!cartItems?.items || cartItems.items.length === 0) {
        return (
            <div className="popup-overlay" onClick={onClose}>
                <div className="popup-content" onClick={(e) => e.stopPropagation()}>
                    <h2>Your Cart</h2>
                    <p>Your cart is empty.</p>
                    <button className="close-button" onClick={onClose}>Close</button>
                </div>
            </div>
        );
    }

    return (
        <div className="popup-overlay" onClick={onClose}>
            <div className="popup-content" onClick={(e) => e.stopPropagation()}>
                <h2>Your Cart</h2>
                <table className="cart-table">
                    <thead>
                    <tr>
                        <th>Product</th>
                        <th>Quantity</th>
                        <th>Price</th>
                        <th>Total</th>
                    </tr>
                    </thead>
                    <tbody>
                    {cartItems.items.map((item) => (
                        <tr key={item.id}>
                            <td>{item.productName || 'N/A'}</td>
                            <td>{item.quantity}</td>
                            <td>{item.price ? `${item.price} DKK` : 'N/A'}</td>
                            <td>{item.totalPrice ? `${item.totalPrice} DKK` : 'N/A'}</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
                <p><strong>Total: {cartItems.total} DKK</strong></p>
                <button className="close-button" onClick={onClose}>Close</button>
            </div>
        </div>
    );
}

export default CartPopup;
