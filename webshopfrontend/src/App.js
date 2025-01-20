import React, { useEffect } from 'react';
import Cookies from 'js-cookie';
import api from './services/Api';
import ProductList from "./components/ProductList"; // Ensure this is correct

function App() {
    useEffect(() => {
        const sessionId = Cookies.get('sessionId');

        // If the sessionId is missing, create one
        if (!sessionId) {
            api.get('/session/init-session')
                .then((response) => {
                    Cookies.set('sessionId', response.data.sessionId, { expires: 1 }); // Expires in 1 day
                    console.log('Session ID initialized:', response.data.sessionId);
                })
                .catch((error) => {
                    console.error('Error initializing session:', error);
                });
        } else {
            console.log('Existing session ID:', sessionId);
        }
    }, []);

    return (
        <div>
            <h1>Webshop</h1>
            {<ProductList/>}
        </div>
    );
}

export default App;
