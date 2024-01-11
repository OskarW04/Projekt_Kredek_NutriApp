import React, { useEffect } from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Login from './Components/Login.jsx'
import Register from './Components/Register.jsx';
import Home from './Components/Home.jsx';
import UserDetails from './Components/UserDetails.jsx';
import Search from './Components/Search.jsx'
import AuthProvider from './Components/AuthProvider.jsx'
import PrivateRoute from './Components/PrivateRoute.jsx';


function App() {
    return (
        <Routes>
            <Route path='/Home' element={<Home/>}/>
            <Route path="/" element={<AuthProvider/>} />
            <Route path="/Login" element={<Login/>} />
            <Route path="/Register" element={<Register/>} />
            <Route exact path='/' element={<PrivateRoute/>}>
                <Route exact path="/Details" element={<UserDetails/>}/>
                <Route exact path='/Search' element={<Search/>}/>
            </Route>
        </Routes>
    );
}

export default App;