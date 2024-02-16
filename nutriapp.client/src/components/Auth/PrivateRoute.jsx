import { Navigate, Outlet } from "react-router-dom";

const Auth = () => {
  const token = sessionStorage.getItem("token");
  if (token) {
    return token;
  } else return null;
};

const PrivateRoute = () => {
  const Authorization = Auth();
  const auth = Authorization;
  return auth ? <Outlet /> : <Navigate to="/Home" />;
};

export default PrivateRoute;
