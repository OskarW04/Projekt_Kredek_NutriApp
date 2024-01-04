import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Login from './Components/Login.jsx'
import Register from './Components/Register.jsx';
import Home from './Components/Home.jsx';
import UserDetails from './Components/UserDetails.jsx';
import Main from './Components/Main.jsx'
import { AuthProvider } from './Components/AuthProvider.jsx';

function App() {
    return (
        <AuthProvider>
                <Routes>
                    <Route path="/" Component={Home} />
                    <Route path="/Login" Component={Login} />
                    <Route path="/Register" Component={Register} />
                    <Route path="/Details" Component={UserDetails}/>
                    <Route path='/Main' Component={Main}/>
                </Routes>
        </AuthProvider>
    );
}

export default App;