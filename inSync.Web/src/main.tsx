import React from 'react';
import ReactDOM from 'react-dom/client';
import { router } from './App';
import Provider from './components/Provider';
import 'antd/dist/reset.css';
import { RouterProvider } from '@tanstack/react-router';

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
    <React.StrictMode>
        <Provider>
            <RouterProvider router={router} />
        </Provider>
    </React.StrictMode>
);
