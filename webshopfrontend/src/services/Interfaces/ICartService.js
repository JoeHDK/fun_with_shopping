import api from '../Api';

const ICartService = {
    getProducts: async () => {
        try {
            const response = await api.get('/products'); // Adjust the endpoint if needed
            return response.data;
        } catch (error) {
            console.error('Error fetching products:', error.response?.data || error.message);
            throw error;
        }
    },
    getCart: async (sessionId) => {
        try {
            const response = await api.get(`/cart/${sessionId}`);
            return response.data;
        } catch (error) {
            console.error('Error fetching cart:', error.response?.data || error.message);
            throw error;
        }
    },
    addToCart: async (cartItem) => {
        try {
            const response = await api.post('/cart', cartItem);
            return response.data;
        } catch (error) {
            console.error('Error adding to cart:', error.response?.data || error.message);
            throw error;
        }
    },
};

export default ICartService;
