import api from "../Api";

const IOrderService = {
    createOrder: async (orderData) => {
        try {
            const { sessionId, items, total } = orderData; 
            const response = await api.post(`/orders/${sessionId}`, { items, total }); 
            return response.data;
        } catch (error) {
            console.error('Error creating order:', error);
            throw error;
        }
    },
};


export default IOrderService;
