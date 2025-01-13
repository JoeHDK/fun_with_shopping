import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:7155/api', // Ensure this matches your backend URL
    headers: {
        'Content-Type': 'application/json',
    },
    withCredentials: true, // Include cookies for session ID
});

export default api;
