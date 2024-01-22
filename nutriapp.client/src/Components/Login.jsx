import React from "react";
import { useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router-dom";
import axios from 'axios';
import "../App.css";

export function Login() {

  const apiUrl = import.meta.env.VITE_API_URL;

  const form = useForm();
  const { register, control, handleSubmit } = form;
  const navigate = useNavigate();

  const onSubmit = async (data) => {
    const json = JSON.stringify(data)
      try {
        
        const response = await axios.post(`${apiUrl}/login`, json, {
          headers:{
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          }
        });


          const token = response.data.accessToken
          sessionStorage.setItem('token', token);
          sessionStorage.setItem('email', data.email);

          var areDetails = true;
          try {
              const details = await axios.get(`${apiUrl}/api/UserDetails`, {
                  headers: { 'Authorization': `Bearer ${token}` },
                  params: { 'email': data.email }
              })
          } catch (error) {
              areDetails = false;
              navigate('/Details')
              
          }
          window.alert('Zalogowano pomyślnie');
          if (areDetails) {
              navigate('/MealPlan')
          }
      }catch (error){
        window.alert('Błąd logowania');
      }
  }

  return (
    <div>
      <h1>Zaloguj się:</h1>
      <form onSubmit={handleSubmit(onSubmit) }>
        <label htmlFor="email">E-mail</label>
        <input type="text" id="email" {...register("email")} />

        <label htmlFor="password">Hasło</label>
        <input type="password" id="password" {...register("password")} />

        <button>Submit</button>
        <p>
          Nie masz konta? <Link to="/Register">Zarejestruj się</Link>{" "}
        </p>
      </form>
    </div>
  );
}
export default Login;
