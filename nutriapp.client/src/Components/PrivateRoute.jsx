import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';

const Auth= () => {
    const token = localStorage.getItem('token');
    if(token)
    {
        return token
    }
    else 
    return null
}


const PrivateRoute = () => {
    const Authorization = Auth()
    const auth = Authorization; // determine if authorized, from context or however you're doing it
    // If authorized, return an outlet that will render child elements
    // If not, return element that will navigate to login page
    return auth ? <Outlet /> : <Navigate to="/login" />;
}

export default PrivateRoute;