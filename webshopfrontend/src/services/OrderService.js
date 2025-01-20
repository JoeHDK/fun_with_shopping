import IOrderService from "./Interfaces/IOrderService";
import api from "./Api";

const OrderService = {
    createOrder: async (sessionId) => {
        await api.post(`/order?sessionId=${sessionId}`);
    },
};

Object.assign(OrderService, IOrderService);

export default OrderService;
