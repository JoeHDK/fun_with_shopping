import './css/CartPopup.css';
import CartService from "../services/CartService";
import Cookies from "js-cookie";

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

    const createOrder = () => {
        const sessionId = Cookies.get('sessionId');
        if (!sessionId) {
            console.error('Session ID not found in cookies.');
            alert('Session ID is missing. Please reload the page.');
            return;
        }

        // Call the backend API to create the order
        CartService.createOrder(sessionId)
            .then(() => {
                alert('Order created successfully!');
                onClose(); // Close the cart popup
            })
            .catch((error) => {
                console.error('Error creating order:', error);
                alert('Failed to create order. Please try again.');
            });
    };

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
                <h3>Total: {cartItems.total} DKK</h3>
                <button className="close-button" onClick={onClose}>Close</button>
                <button className="create-order-button" onClick={createOrder}>
                    Place Order
                </button>
            </div>
        </div>
    );
}

export default CartPopup;
