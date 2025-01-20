import React, { useEffect } from 'react';
import './css/CartPopup.css';
import Cookies from "js-cookie";
import IOrderService from "../services/Interfaces/IOrderService";

function CartPopup({ cartItems, onClose }) {
    useEffect(() => {
        console.log('CartPopup received cartItems:', cartItems); // Log cartItems to debug
    }, [cartItems]);

    if (!cartItems || !cartItems.items) {
        return <div>No items in cart.</div>;
    }
    
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
            alert('Session ID is missing. Please reload the page.');
            return;
        }

        const orderData = {
            sessionId,
            items: cartItems.items,
            total: cartItems.total,
        };

        console.log("Order Data:", orderData);

        IOrderService.createOrder(orderData)
            .then(() => {
                alert('Order created successfully!');
                onClose();
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
                            <td>{item.productName}</td>
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
