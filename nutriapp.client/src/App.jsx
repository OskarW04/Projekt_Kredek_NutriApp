import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Login from './Components/Login.jsx';
import Register from './Components/Register.jsx';
import Home from './Components/Home.jsx';
import UserDetails from './Components/UserDetails.jsx';

function App() {
    return (
        <Routes>
            <Route path="/" element={<Home /> } />
            <Route path="/Login" element={<Login />} />
            <Route path="/Register" element={<Register />} />
            <Route path="/Details" element={<UserDetails />} />
        </Routes>
    );
}

export default App;