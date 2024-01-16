import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

const AuthProvider = () => {
  const navigate = useNavigate();

  useEffect(() => {
    // Sprawdź, czy token jest zapisany w local storage
    const token = sessionStorage.getItem('token');

    if (token) {
      // Jeżeli token istnieje, przekieruj na stronę /main
      navigate('/MealPlan');
    } else {
      // Jeżeli token nie istnieje, przekieruj na stronę /login
      navigate('/Home');
    }
  }, []); // Pusta tablica dependencies, aby sprawdzić token tylko raz przy montowaniu

  // Placeholder - ten komponent nie musi renderować niczego na stronie
  return null;
};

export default AuthProvider;