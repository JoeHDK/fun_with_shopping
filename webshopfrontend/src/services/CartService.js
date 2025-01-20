import ICartService from './Interfaces/ICartService';
import api from "./Api";

const CartService = {
    getProducts: async () => {
        const response = await api.get('/products');
        return response.data;
    },
    getCart: async (sessionId) => {
        const response = await api.get(`/cart?sessionId=${sessionId}`);
        return response.data;
    },
    addToCart: async (cartItem) => {
        await api.post('/cart', cartItem);
    },
};

Object.assign(CartService, ICartService);

export default CartService;
