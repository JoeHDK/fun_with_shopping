import React, { useEffect } from 'react';
import Cookies from 'js-cookie';
import api from './services/Api';
import ProductList from "./components/ProductList";

function App() {
    useEffect(() => {
        const existingSessionId = Cookies.get('sessionId');
        if (!existingSessionId) {
            api.get('/session/init-session')
                .then((response) => {
                    console.log('Session initialized:', response.data.sessionId);
                    Cookies.set('sessionId', response.data.sessionId, { expires: 0.0104 });
                })
                .catch((error) => {
                    console.error('Error initializing session:', error);
                });
        } else {
            console.log('Existing session ID:', existingSessionId);
        }

    }, []);

    return (
        <div className="App">
            <header>
                <h1>Welcome to the Webshop</h1>
            </header>
            <main>
                <ProductList />
            </main>
        </div>
    );
}

export default App;
