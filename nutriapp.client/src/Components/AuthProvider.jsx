import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";

const AuthProvider = () => {
  const navigate = useNavigate();

  useEffect(() => {
    const token = sessionStorage.getItem("token");
    if (token) {
      navigate("/MealPlan");
    } else {
      navigate("/Home");
    }
  }, []);

  return null;
};

export default AuthProvider;
