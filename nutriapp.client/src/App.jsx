import React, { useEffect, useState } from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Login from "./Components/Login.jsx";
import Register from "./Components/Register.jsx";
import Home from "./Components/Home.jsx";
import UserDetails from "./Components/UserDetails.jsx";
import { UserDetailsUpdate } from "./Components/UserDetailsUpdate.jsx";
import { UserDetailsGet } from "./Components/UserDetailsGet.jsx";
import Search from "./Components/Search.jsx";
import MealPlan from "./Components/MealPlan.jsx";
import AuthProvider from "./Components/AuthProvider.jsx";
import PrivateRoute from "./Components/PrivateRoute.jsx";
import AddDish from "./Components/AddDish.jsx"
import CreateDish from "./Components/CreateDish.jsx";

function App() {

  return (
    <Routes>
      <Route path="/Home" element={<Home />} />
      <Route path="/" element={<AuthProvider />} />
      <Route path="/Login" element={<Login />} />
      <Route path="/Register" element={<Register />} />
      <Route exact path="/" element={<PrivateRoute />}>
        <Route exact path="/Details" element={<UserDetails />} />
        <Route exact path="/Details/Update" element={<UserDetailsUpdate />} />
        <Route exact path="/Details/Get" element={<UserDetailsGet />} />
        <Route exact path="/Search/:adress" element={<Search />} />
        <Route exact path="/MealPlan" element={<MealPlan />} />
        <Route exact path="/AddDish" element={<AddDish/>} />
        <Route exact path ="/CreateDish/:location" element={<CreateDish/>} />
      </Route>
    </Routes>
  );
}

export default App;
