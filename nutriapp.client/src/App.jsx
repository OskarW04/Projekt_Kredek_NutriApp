import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import AuthProvider from "./components/Auth/AuthProvider.jsx";
import PrivateRoute from "./components/Auth/PrivateRoute.jsx";
import Login from "./pages/Login.jsx";
import Register from "./pages/Register.jsx";
import Home from "./pages/Home.jsx";
import UserDetails from "./pages/UserDetails.jsx";
import UserDetailsUpdate from "./pages/UserDetailsUpdate.jsx";
import UserDetailsGet from "./pages/UserDetailsGet.jsx";
import Search from "./pages/Search.jsx";
import MealPlan from "./pages/MealPlan.jsx";
import AddDish from "./pages/AddDish.jsx";
import CreateDish from "./pages/CreateDish.jsx";
import AddProduct from "./pages/AddProduct.jsx";

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
        <Route exact path="/AddDish" element={<AddDish />} />
        <Route exact path="/CreateDish/:location" element={<CreateDish />} />
        <Route exact path="/AddProduct/:adress" element={<AddProduct />} />
      </Route>
    </Routes>
  );
}

export default App;
