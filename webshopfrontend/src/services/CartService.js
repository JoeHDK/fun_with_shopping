import api from './Api';

const CartService = {
    getProducts: () => {
        return api.get('/products'); // Fetch products
    },
    getCart: (sessionId) => {
        return api.get(`/cart/${sessionId}`); // Fetch cart
    },
    addToCart: (cartItem) => {
        return api.post('/cart', cartItem); // Add to cart
    },
};

export default CartService;
