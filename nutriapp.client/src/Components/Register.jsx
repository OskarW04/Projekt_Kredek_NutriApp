import React, { useRef } from "react";
import { useForm } from "react-hook-form";
import { useNavigate, Link } from "react-router-dom";
import  axios  from "axios"

const Register = () => {

  const apiUrl = import.meta.env.VITE_API_URL;

  const form = useForm();
  const navigate = useNavigate();
  const { register, control, handleSubmit, watch, formState } = form;
  const { errors } = formState;
  const password = useRef({});
  password.current = watch("password", "");

    const onSubmit = async ({repeatPassword,...data}) => {
      const json = JSON.stringify(data)
        try{
          const response = await axios.post(`${apiUrl}/register`, json, {
            headers:{
              'Accept': 'application/json',
              'Content-Type': 'application/json'
            }
          });
          
          navigate("/login")
          window.alert("Pomyślnie zarejestrowano");
          
        }catch (error){
          window.alert("Błąd rejestracji");
        }
    }

  return (
    <div>
      <h1>Zarejestruj sie:</h1>
        <form onSubmit={handleSubmit(onSubmit) } noValidate>
        <div className="form-control">
        <label htmlFor="email">E-mail</label>
        <input type="text" id="email" {...register("email", {
          required: {
            value: true,
            message: 'Email jest wymagany.'},
          pattern: {
          value: /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
          message: "Niepoprawny format adresu email"
        }
        })} />
        <p className="error">{errors.email?.message}</p>
        </div>

        <div className="form-control">
        <label htmlFor="password">Hasło</label>
        <input type="password" id="password" {...register("password", {
          
          required: {
            value: true,
            message: 'Hasło jest wymagane.'},
          minLength:{
            value:6,
            message: "Twoje hasło musi składać się z conajmniej 6 znaków."
          },
          validate:{
            hasLower: (value) => /[a-z]/.test(value) || 'Twoje hasło musi posiadać minimum jedną mała literę (a-z)',
            hasUpper: (value) => /[A-Z]/.test(value) || 'Twoje hasło musi posiadać minimum jedną wielką literę (A-Z)',
            hasNumber: (value) => /[0-9]/.test(value) || 'Twoje hasło musi zawierać cyfrę',
            hasSpecialChar: (value) => /[$&+,:;=?@#|'<>.^*()%!-]/.test(value) || 'Twoje hasło musi zawierać znak specjalny'
            },
          }
        )} />
        <p className="error">{errors.password?.message}</p>
        </div>
        
        <div className="form-control">
        <label htmlFor="repeatPassword">Powtórz hasło</label>
        <input type="password" id="repeatPassword" {...register("repeatPassword", {
          validateCriteriaMode: "all",
          required: {
            value: true,
            message: "Powtórz hasło."},
          validate: value => 
          value === password.current || "Hasła nie są takie same."
        })}/>
        <p className="error">{errors.repeatPassword?.message}</p>
        </div>
      
        <button>Zarejestruj</button>
        <p>
          Masz juz konto? <Link to="/Login">Zaloguj się</Link>
        </p>
      </form>
    </div>
  );
}
export default Register;
